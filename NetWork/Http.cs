using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BianCore.NetWork
{
    public class Http
    {
        private HttpClient HttpClient = new HttpClient();
        public async Task<HttpResponseMessage> HttpGetAsync(string url, string ContentType = "application/json", Tuple<string, string> AuthTuple = default)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url); ;
            if (AuthTuple != null)
            {
                message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(AuthTuple.Item1, AuthTuple.Item2);
            }
            var responseMessage = await HttpClient.SendAsync(message, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
            if (responseMessage.StatusCode.Equals(HttpStatusCode.Found))
            {
                string redirectUrl = responseMessage.Headers.Location.AbsoluteUri;
                responseMessage.Dispose();
                GC.Collect();
                return await HttpGetAsync(redirectUrl, AuthTuple: AuthTuple, ContentType: ContentType);
            }
            return responseMessage;
        }
        public Http()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }
        public async Task<HttpResponseMessage> HttpPostAsync(string url, string content, string ContentType = "application/json")
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);
            var PostContent = new StringContent(content);
            PostContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(ContentType);
            message.Content = PostContent;
            var res = await HttpClient.SendAsync(message);
            message.Dispose();
            PostContent.Dispose();
            return res;
        }

        public async Task<HttpResponseMessage> HttpPostAsync(string url, string ContentType = "application/json")
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);
            var PostContent = new StringContent("");
            PostContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(ContentType);
            message.Content = PostContent;
            var res = await HttpClient.SendAsync(message);
            message.Dispose();
            PostContent.Dispose();
            return res;
        }
        public async Task<HttpResponseMessage> HttpPostAsync(string url, Dictionary<string, string> content, string ContentType = "application/x-www-form-urlencoded")
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);
            var PostContent = new FormUrlEncodedContent(content);
            PostContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(ContentType);
            message.Content = PostContent;
            var res = await HttpClient.SendAsync(message);
            message.Dispose();
            PostContent.Dispose();
            return res;
        }
    }
}
