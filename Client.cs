using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WolfyBot.Core.Enums;

namespace WolfyBot
{
    public class Client
    {
        public WebSocketSharp.WebSocket ws = null;
        public List<WebSocketSharp.Net.Cookie> HandShakeCookies = new List<WebSocketSharp.Net.Cookie>();
        public string SID;
        public string UserToken;
        public StatesEnum ClientStatus;
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
                ws.Send("2");
                timer.Change(interval, Timeout.Infinite);
            },
            null, 25000, Timeout.Infinite);
        }

        private async Task Connection(string sid)
        {
            using (ws = new WebSocket(url: "wss://wolfy.fr/socket.io/?token=8cb85bca-72bb-4722-bc16-1548e2e45eed&EIO=3&transport=websocket&sid=" + sid))
            {
                SetupPing(ws);
                ws.Origin = "https://wolfy.fr";
                Console.WriteLine(ws.Url);
                foreach (var cook in HandShakeCookies)
                {
                    ws.SetCookie(cook);
                }
                ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                ws.OnMessage += client_OnMessage;
                ws.OnError += client_OnError;
                ws.OnClose += client_OnClose;
                ws.Compression = CompressionMethod.Deflate;
                ws.Connect();
                ws.Send("2probe");
                ws.Send("5");
                Console.ReadKey(true);
            }
            Console.ReadLine();
        }

        public void client_OnMessage(object sender, MessageEventArgs e)
        {
            string response = e.Data;
            Console.WriteLine("client_OnMessage : " + response);              
        }

        public void client_OnClose(object sender, CloseEventArgs e)
        {
            string response = e.Reason;
            Console.WriteLine("client_OnClose : " + response);
        }

        public void client_OnError(object sender, ErrorEventArgs e)
        {
            string response = e.Message;
            Console.WriteLine("client_OnError : " + response + " " + e.Exception);
        }
    }
}