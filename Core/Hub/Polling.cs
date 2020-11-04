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
        public CookieContainer cookieContainer = new CookieContainer();

        public HttpClientHandler hq = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.All,
        };

        public HttpClient WebClient = null;
        public string UserToken;

        public Polling(string userToken, Client client)
        {
            this.UserToken = userToken;
            hq.CookieContainer = cookieContainer;
            hq.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            WebClient = new HttpClient(hq);
            string rand = StringHelper.RandomString(7);
            string sid = GetSID(rand).Result;

            UseSID(rand, sid).Wait();
            GetAfterPost(rand, sid, client).Wait();
            client.SID = sid;
        }

        public async void WorldPolling(Client client, string worldid, string worldinstanceid)
        {
            cookieContainer = new CookieContainer();
            hq = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.All,
            };
            hq.CookieContainer = cookieContainer;
            hq.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            WebClient = new HttpClient(hq);
            string rand = StringHelper.RandomString(7);
            string sid = await GetSID(rand, true, worldid, worldinstanceid);
            await GetAfterPost(rand, sid, client, true, worldid, worldinstanceid);
            client.CurrentGameId = worldid;
            client.CurrentGameInstanceId = worldinstanceid;
            client.SID = sid;
        }

        private async Task<string> GetSID(string randstring, bool world = false, string worldid = null, string worldinstanceid = null)
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
            HttpResponseMessage responsetoken;
            if (!world)
                responsetoken = await Requests.Get(WebClient, "https://wolfy.fr/socket.io/?token=" + UserToken + "&EIO=3&transport=polling&t=" + randstring, HeaderAccessToken);
            else
                responsetoken = await Requests.Get(WebClient, "https://wolfy.fr/instance/" + worldinstanceid + "/socket.io/?token=" + UserToken + "&gameId=" + worldid + "&EIO=3&transport=polling&t=" + randstring, HeaderAccessToken);
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
            Program.WriteColoredLine($"[{DateTime.Now.ToString("HH:mm:ss")}] Got SID : {obj["sid"].ToString()}", ConsoleColor.Green);
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

        private async Task<string> GetAfterPost(string randstring, string sid, Client client, bool world = false, string worldid = null, string worldinstanceid = null)
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
            HttpResponseMessage responsetoken;
            Uri uri;
            if (!world)
            {
                responsetoken = await Requests.Get(WebClient, "https://wolfy.fr/socket.io/?token=" + UserToken + "&EIO=3&transport=polling&t=" + randstring + ".0" + "&sid=" + sid, HeaderAccessToken);
                uri = new Uri("https://wolfy.fr/socket.io/?token=" + UserToken + "&EIO=3&transport=polling&t=" + randstring + ".0" + "&sid=" + sid);
            }
            else
            {
                responsetoken = await Requests.Get(WebClient, "https://wolfy.fr/instance/" + worldinstanceid + "/socket.io/?token=" + UserToken + "&gameId=" + worldid + "&EIO=3&transport=polling&t=" + randstring + ".0" + "&sid=" + sid, HeaderAccessToken);
                uri = new Uri("https://wolfy.fr/instance/" + worldinstanceid + "/socket.io/?token=" + UserToken + "&gameId=" + worldid + "&EIO=3&transport=polling&t=" + randstring + ".0" + "&sid=" + sid);
            }
            string response = responsetoken.Content.ReadAsStringAsync().Result;

            IEnumerable<Cookie> responseCookies = cookieContainer.GetCookies(uri).Cast<Cookie>();
            client.HandShakeCookies.Clear();
            foreach (Cookie cookie in responseCookies)
            {
                Console.WriteLine($"[{DateTime.Now.ToString("HH: mm:ss")}] We got cookies : " + cookie.Name + " -> " + cookie.Value);
                client.HandShakeCookies.Add(new WebSocketSharp.Net.Cookie(cookie.Name, cookie.Value));
            }
            client.CurrentNetworkState = NetworkEnum.LOGGING_IN;
            WebClient.Dispose();
            WebClient = null;
            return response;
        }
    }
}