using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WolfyBot.Core.Dispatcher;
using WolfyBot.Core.Enums;
using WolfyBot.Core.Game;

namespace WolfyBot
{
    public class Client
    {
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
        public bool wasWsCloseExpected = false;

        public Client(string userToken)
        {
            this.ClientStatus = StatesEnum.NONE;
            this.CurrentNetworkState = NetworkEnum.DISCONNECTED;
            this.UserToken = userToken;
        }

        public void ConnectToHub()
        {
            Poll = new Polling(UserToken, this);
            Program.WriteColoredLine($"[{ DateTime.Now.ToString("HH:mm:ss")}] Connecting to Hub..", ConsoleColor.Magenta);
            WsConnection(SID).Wait();
        }

        public void ConnectToWorld(string worldid, string worldinstanceid)
        {
            wasWsCloseExpected = true;
            Program.WriteColoredLine($"[{ DateTime.Now.ToString("HH:mm:ss")}] Switching to world..", ConsoleColor.Magenta);
            Poll.WorldPolling(this, worldid, worldinstanceid);
            Thread.Sleep(500);
            WsConnection(SID, true, worldid, worldinstanceid).Wait();
        }

        private void SetupPing(WebSocket ws)
        {
            var interval = 25000;
            Timer timer = null;
            timer = new Timer(state =>
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

        private async Task WsConnection(string sid, bool world = false, string worldid = null, string worldinstanceid = null)
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
            if (CurrentNetworkState == NetworkEnum.LOGGING_IN)
                CurrentNetworkState = NetworkEnum.LOGGED_HUB;
            else
                CurrentNetworkState = NetworkEnum.LOGGED_GAME;
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Connection opened at " + ws.Url, ConsoleColor.Blue);

            GameHub = new Hub();
        }

        public void client_OnMessage(object sender, MessageEventArgs e)
        {
            Program.TotalNetworkReceivedLength += ulong.Parse(e.RawData.Length.ToString());
            string response = e.Data;
            if (!response.Contains("hydrateFriendRequests") && response != "3probe" && response != "3" && response != "41") // on remove les messages ws
                Reader.MessageReader(this, response);
            else
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] RCV -> {response}", ConsoleColor.DarkCyan);

            // TEMP USED TO SWITCH FROM HUB TO GAME

            if (response.Contains("game_create"))
            {
                string message = response;
                while (!message.StartsWith("[")) //TODO improve this part
                {
                    message = message.Remove(0, 1);
                }
                while (!message.EndsWith("]"))
                {
                    message = message.Remove(message.Length - 1, 1);
                }
                string packetname = message.Substring(2, message.IndexOf(",{") - 3);
                string json = message.Replace($"[\"{packetname}\",", "");
                json = json.Replace("]", "");
                var jsonobj = JObject.Parse(json);
                string id = jsonobj["id"].ToString();
                string instanceId = jsonobj["instanceId"].ToString();
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] A game has been created, joining it ! GameID : {id}, GameInstanceID : {instanceId}", ConsoleColor.Cyan);
                Quit();
                ConnectToWorld(id, instanceId);
            }
        }

        public void client_OnClose(object sender, CloseEventArgs e)
        {
            if (!wasWsCloseExpected)
                CurrentNetworkState = NetworkEnum.DISCONNECTED;
            else
                wasWsCloseExpected = !wasWsCloseExpected;
            

            string response = e.Reason;
            if (response.Length == 0)
                return;
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] client_OnClose : " + response, ConsoleColor.Red);
        }

        public void client_OnError(object sender, ErrorEventArgs e)
        {
            string response = e.Message;
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] client_OnError : " + response + " " + e.Exception, ConsoleColor.Red);
        }

        public void SendMessage(string msg, int delay = 0)
        {
            if (Disposed || !ws.IsConnected || ws == null)
                return;

            if (delay != 0)
                Thread.Sleep(delay);

            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] SND -> {msg}", ConsoleColor.Green);
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
        }
    }
}