using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using WebSocketSharp;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Enums;
using WolfyBot.Core.Game;

namespace WolfyBot
{
    public class Client
    {
        public int ClientInstanceid = 0;
        public WebSocketSharp.WebSocket ws = null;
        public List<WebSocketSharp.Net.Cookie> HandShakeCookies = new List<WebSocketSharp.Net.Cookie>();
        public string SID;
        public string UserToken;
        public StatesEnum ClientStatus;
        public NetworkEnum CurrentNetworkState;
        public bool Disposed = false;
        public Polling Poll = null;
        public string CurrentGameId = "";
        public string CurrentGameInstanceId = "";
        public Hub GameHub;
        public GameIA InGameIA;
        public bool wasWsCloseExpected = false;
        public int TotalEloEarned = 0;
        public string Userid;
        public bool isWaitingToConnect = true;
        public bool isCurrentlyConnecting = false;
        private int PartyCount = 0;
        public Client(string userToken, string userid)
        {
            this.ClientInstanceid = ClientsManager.ClientList.Count();
            this.Userid = userid;
            this.ClientStatus = StatesEnum.NONE;
            this.CurrentNetworkState = NetworkEnum.DISCONNECTED;
            this.UserToken = userToken;
            this.InGameIA = new GameIA(this);
        }

        public void ConnectToHub()
        {
            isCurrentlyConnecting = true;
            Poll = new Polling(UserToken, this);
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Connecting to Hub..", ConsoleColor.Magenta, this);
            WsConnection(SID);
        }

        public void ConnectToWorld(string worldid, string worldinstanceid)
        {
            InGameIA.GameStartTimer.Elapsed += new ElapsedEventHandler(InGameIA.GameStartTimerElasped);
            InGameIA.GameStartTimer.Interval = 120000; // 120 s timeout for a party to start
            InGameIA.GameStartTimer.Enabled = true;

            wasWsCloseExpected = true;
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Switching to world..", ConsoleColor.Magenta, this);
            Poll.WorldPolling(this, worldid, worldinstanceid);
            Thread.Sleep(1000);
            WsConnection(SID, true, worldid, worldinstanceid);
        }

        public void Reconnect()
        {
            PartyCount++;
            Dispose();
            InGameIA.Dispose();
            ws.Close();           
            isWaitingToConnect = true;
        }

        private void SetupPing(WebSocket ws)
        {
            var interval = 25000;
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer(state =>
            {
                if (!ws.IsConnected)
                    timer.Dispose();
                else
                {
                    SendMessage("2");
                    timer.Change(interval, Timeout.Infinite);
                }
            },
            null, 25000, Timeout.Infinite);
        }

        private void WsConnection(string sid, bool world = false, string worldid = null, string worldinstanceid = null)
        {
            string Url;
            if (!world)
                Url = "wss://wolfy.fr/socket.io/?token=" + UserToken + "&EIO=3&transport=websocket&sid=" + sid;
            else
                Url = $"wss://wolfy.fr/instance/{worldinstanceid}/socket.io/?token={UserToken}&gameId={worldid}&EIO=3&transport=websocket&sid={sid}";
            using (ws = new WebSocket(url: Url))
            {
                SetupPing(ws);
                ws.Origin = "https://wolfy.fr";
                foreach (var cook in HandShakeCookies)
                {
                    ws.SetCookie(cook);
                }

                ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                ws.OnMessage += client_OnMessage;
                ws.OnError += client_OnError;
                ws.OnClose += client_OnClose;
                ws.OnOpen += client_OnOpen;
                ws.Compression = CompressionMethod.Deflate;
                ws.Connect(sid);
                SendMessage("2probe");
                SendMessage("5");
                Console.ReadKey(true);
            }
            Console.ReadLine();
        }

        public void client_OnOpen(object sender, EventArgs e)
        {
            if (CurrentNetworkState == NetworkEnum.LOGGING_IN && !wasWsCloseExpected)
                CurrentNetworkState = NetworkEnum.LOGGED_HUB;
            else if (CurrentNetworkState == NetworkEnum.LOGGING_IN && wasWsCloseExpected)
            {
                wasWsCloseExpected = !wasWsCloseExpected;
                CurrentNetworkState = NetworkEnum.LOGGED_GAME;
                isWaitingToConnect = false;
                isCurrentlyConnecting = false;
            }

            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Connection opened at " + ws.Url, ConsoleColor.Blue, this);

            // GameHub = new Hub();
        }

        public void client_OnMessage(object sender, MessageEventArgs e)
        {
            Program.TotalNetworkReceivedLength += ulong.Parse(e.RawData.Length.ToString());
            string response = e.Data;

            //Console.WriteLine(response);

            if (response == "3probe" || response == "3" || response == "41") // websocket
                return;

            if (!response.Contains("{"))
            {
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] RCV STRANGE MSG -> {response}", ConsoleColor.White, this);// not json form
                Reader.StrangeMessageReader(this, response);
                return;
            }

            if (response.StartsWith("42") || response.StartsWith("42/hub,[\"authenticated\"]") || response.StartsWith("42/hub,[\"hydrateFrien"))
                Reader.MessageReader(this, response);
            else
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] RCV -> {response}", ConsoleColor.Red, this);
        }

        public void client_OnClose(object sender, CloseEventArgs e)
        {
            if (wasWsCloseExpected)
                return;
            if (!wasWsCloseExpected) 
                CurrentNetworkState = NetworkEnum.DISCONNECTED;

            string response = e.Reason;
            if (response.Length == 0)
                return;
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] client_OnClose : " + response, ConsoleColor.Red, this);
           // Reconnect();
        }

        public void client_OnError(object sender, ErrorEventArgs e)
        {
            string response = e.Message;
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] client_OnError : " + response + " " + e.Exception, ConsoleColor.Red, this);
        }

        public void SendMessage(string msg, int delay = 0)
        {
            if (Disposed || !ws.IsConnected || ws == null)
                return;

            if (delay != 0)
                Thread.Sleep(delay);

            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] SND -> {msg}", ConsoleColor.Green, this);
            try
            {
                ws?.Send(msg);
                Program.TotalNetworkSentLength += ulong.Parse(msg.Length.ToString());
            }
            catch { }
        }

        public void Quit()
        {
            ws.Close();
        }

        public void Dispose()
        {
            CurrentGameId = "";
            CurrentGameInstanceId = "";
        }
    }
}