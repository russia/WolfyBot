using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WolfyBot.Helpers
{
    internal class Requests
    {
        public static async Task<HttpResponseMessage> Post(HttpClient httpClient, string uri, HttpContent content, Dictionary<string, string> customHeader)
        {
            httpClient.CancelPendingRequests();
            httpClient.DefaultRequestHeaders.Clear();

            foreach (var header in customHeader)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        retry:
            try
            {
                var message = await httpClient.PostAsync(uri, content);
                return message;
            }
            catch (Exception ex)
            {
                goto retry;
            }
        }

        public static async Task<HttpResponseMessage> Get(HttpClient httpClient, string uri, Dictionary<string, string> customHeader)
        {
            httpClient.CancelPendingRequests();
            httpClient.DefaultRequestHeaders.Clear();

            foreach (var header in customHeader)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            byte retries = 0;
        retry:
            try
            {
                var message = await httpClient.GetAsync(uri);
                return message;
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000);
                retries++;
                goto retry;
            }
        }

        public static async Task<HttpResponseMessage> HandShake(HttpClient httpClient, string uri, Dictionary<string, string> customHeader)
        {
            httpClient.CancelPendingRequests();
            httpClient.DefaultRequestHeaders.Clear();

            foreach (var header in customHeader)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            try
            {
                var message = await httpClient.GetAsync(uri);
                return message;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}