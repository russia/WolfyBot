using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WolfyBot.Core.Enums;
using WolfyBot.Core.MessageReader;

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

        public Client(string userToken)
        {
            this.ClientStatus = StatesEnum.NONE;
            this.UserToken = userToken;
            Polling Poll = new Polling(UserToken, this);
            Connection(SID).Wait();
        }

        private void SetupPing(WebSocket ws)
        {
            var interval = 25000;
            Timer timer = null;
            timer = new Timer(state =>
            {
                SendMessage("2");
                timer.Change(interval, Timeout.Infinite);
            },
            null, 25000, Timeout.Infinite);
        }

        private async Task Connection(string sid)
        {
            using (ws = new WebSocket(url: "wss://wolfy.fr/socket.io/?token=" + UserToken + "&EIO=3&transport=websocket&sid=" + sid))
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
                ws.Connect();
                SendMessage("2probe");
                SendMessage("5");
                Console.ReadKey(true);
            }
            Console.ReadLine();
        }

        public void client_OnOpen(object sender, EventArgs e)
        {
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Connection opened at " + ws.Url, ConsoleColor.Blue);
            CurrentNetworkState = NetworkEnum.LOGGED_HUB;
        }

        public void client_OnMessage(object sender, MessageEventArgs e)
        {
            string response = e.Data;
            if (!response.Contains("hydrateFriendRequests") && response != "3probe" && response !=  "3")
                Reader.MessageReader(response);
            else
                Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] RCV -> {response}", ConsoleColor.DarkCyan);
        }

        public void client_OnClose(object sender, CloseEventArgs e)
        {
            string response = e.Reason;
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
            }
            catch { }
        }
    }
}