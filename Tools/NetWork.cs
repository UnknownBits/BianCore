using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Net.Http.Headers;

namespace BianCore.Tools
{
    public class Network
    {
        private HttpClient HttpClient = new HttpClient();
        public async Task<HttpResponseMessage> HttpGetAsync(string url, string content_type = "application/json", Dictionary<string, string> headerPairs = null)
        {
            using HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url);
            message.Content.Headers.ContentType = new MediaTypeHeaderValue(content_type);
            if (headerPairs != null)
            {
                foreach (var pair in headerPairs) message.Headers.Add(pair.Key, pair.Value);
            }
            var responseMessage = await HttpClient.SendAsync(message);
            return responseMessage;
        }
        public HttpResponseMessage HttpGet(string url, string content_type = "application/json", Dictionary<string, string> headerPairs = null)
        {
            using HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url);
            message.Content.Headers.ContentType = new MediaTypeHeaderValue(content_type);
            if (headerPairs != null)
            {
                foreach (var pair in headerPairs) message.Headers.Add(pair.Key, pair.Value);
            }
            var responseMessage =  HttpClient.SendAsync(message).Result;
            return responseMessage;
        }
        public Network() { }

        public async Task<HttpResponseMessage> HttpPostAsync(string url, string content, string content_type = "application/json", Dictionary<string, string> headerPairs = null)
        {
            using var strContent = new StringContent(content);
            strContent.Headers.ContentType = new MediaTypeHeaderValue(content_type);
            return await HttpPostAsync(url, strContent, headerPairs);
        }

        public async Task<HttpResponseMessage> HttpPostAsync(string url, HttpContent content, Dictionary<string, string> headerPairs = null)
        {
            using HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Content = content;
            if (headerPairs != null)
            {
                foreach (var pair in headerPairs) message.Headers.Add(pair.Key, pair.Value);
            }
            var res = await HttpClient.SendAsync(message);
            return res;
        }

        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="IP">需要Ping的IP</param>
        /// <returns> PingReply </returns>
        public static async Task<PingReply> Ping(string IP)
        {
            var pingReply = await Task.Run(() => {
                Ping ping = new Ping();
                PingReply pingReplys = ping.Send(IP, 1000);
                return pingReplys;
            });
            return pingReply;
        }
    }
}
