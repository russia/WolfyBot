using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WolfyBot.Core.Enums;
using WolfyBot.Helper;
using WolfyBot.Helpers;

namespace WolfyBot
{
    public class Polling
    {
        public CookieContainer cookieContainer = null;
        public HttpClient WebClient = null;
        public string UserToken;

        public Polling(string userToken, Client client)
        {
            this.UserToken = userToken;
            cookieContainer = new CookieContainer();
            HttpClientHandler hq = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.All,
                CookieContainer = cookieContainer
            };
            hq.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            WebClient = new HttpClient(hq);
            string rand = StringHelper.RandomString(7);
            string sid = GetSID(rand).Result;

            UseSID(rand, sid).Wait();
            GetAfterPost(rand, sid, client).Wait();
            client.SID = sid;
        }

        private async Task<string> GetSID(string randstring)
        {
            Dictionary<string, string> HeaderAccessToken = new Dictionary<string, string>
                {
                    {"Host","wolfy.fr"},
                    {"user-agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:83.0) Gecko/20100101 Firefox/83.0"},
                    {"accept","*/*"},
                    {"accept-language","fr,fr-FR;q=0.8,en-US;q=0.5,en;q=0.3"},
                    {"accept-encoding","gzip, deflate, br"},
                    {"connection","keep-alive"},
                    {"referer","https://wolfy.fr/play"},
                };
            var responsetoken = await Requests.Get(WebClient, "https://wolfy.fr/socket.io/?token=" + UserToken + "&EIO=3&transport=polling&t=" + randstring, HeaderAccessToken);
            string response = responsetoken.Content.ReadAsStringAsync().Result;
            while (!response.StartsWith("{")) //TODO improve this part
            {
                response = response.Remove(0, 1);
            }
            while (!response.EndsWith("}"))
            {
                response = response.Remove(response.Length - 1, 1);
            }
            var obj = JObject.Parse(response);
            Program.WriteColoredLine($"[] Got SID : {obj["sid"].ToString()}",ConsoleColor.Green);
            return obj["sid"].ToString();
        }

        private async Task<string> UseSID(string randstring, string sid)
        {
            Console.WriteLine(randstring + "  " + sid);

            Dictionary<string, string> HeaderAccessToken = new Dictionary<string, string>
                {
                    {"Host","wolfy.fr"},
                    {"user-agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:83.0) Gecko/20100101 Firefox/83.0"},
                    {"accept","*/*"},
                    {"accept-language","fr,fr-FR;q=0.8,en-US;q=0.5,en;q=0.3"},
                    {"accept-encoding","gzip, deflate, br"},
                    {"Origin","https://wolfy.fr"},
                    {"connection","keep-alive"},
                    {"referer","https://wolfy.fr/play"},
                };

            var contentaccesstoken = new StringContent("50:40/hub?token=" + UserToken + ",", Encoding.UTF8, "text/plain");
            var responsetoken = await Requests.Post(WebClient, "https://wolfy.fr/socket.io/?token=" + UserToken + "&EIO=3&transport=polling&t=" + randstring + "&sid=" + sid, contentaccesstoken, HeaderAccessToken);
            return responsetoken.Content.ReadAsStringAsync().Result;
        }

        private async Task<string> GetAfterPost(string randstring, string sid, Client client)
        {
            Dictionary<string, string> HeaderAccessToken = new Dictionary<string, string>
                {
                    {"Host","wolfy.fr"},
                    {"user-agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:83.0) Gecko/20100101 Firefox/83.0"},
                    {"accept","*/*"},
                    {"accept-language","fr,fr-FR;q=0.8,en-US;q=0.5,en;q=0.3"},
                    {"accept-encoding","gzip, deflate, br"},
                    {"connection","keep-alive"},
                    {"referer","https://wolfy.fr/play"},
                };
            var responsetoken = await Requests.Get(WebClient, "https://wolfy.fr/socket.io/?token=" + UserToken + "&EIO=3&transport=polling&t=" + randstring + ".0" + "&sid=" + sid, HeaderAccessToken);
            string response = responsetoken.Content.ReadAsStringAsync().Result;
            Uri uri = new Uri("https://wolfy.fr/socket.io/?token=" + UserToken + "&EIO=3&transport=polling&t=" + randstring + ".0" + "&sid=" + sid);
            IEnumerable<Cookie> responseCookies = cookieContainer.GetCookies(uri).Cast<Cookie>();
            foreach (Cookie cookie in responseCookies)
            {
                client.HandShakeCookies.Add(new WebSocketSharp.Net.Cookie(cookie.Name, cookie.Value));
            }
            client.HandShakeCookies.Add(new WebSocketSharp.Net.Cookie("io", sid));
            client.HandShakeCookies.Add(new WebSocketSharp.Net.Cookie("connect.sid", "s%3AvVytm5pAgIuNbfJtvEyekMl238U7LZOS.6zg5vpz0xDe9hq8pH23RvRIaC8%2F6dbEyedSbrPbLcsU"));
            client.HandShakeCookies.Add(new WebSocketSharp.Net.Cookie("__stripe_mid", "3992e4be-4eb7-4430-872a-479e1e8d514f8187c8"));
            client.HandShakeCookies.Add(new WebSocketSharp.Net.Cookie("__stripe_sid", "fdd89f83-cf7e-45f9-8233-ba19746ff587531737"));
            client.CurrentNetworkState = NetworkEnum.LOGGING_IN;
            return response;
        }
    }
}
