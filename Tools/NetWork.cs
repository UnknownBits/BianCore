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
    public static class Network
    {
        /// <summary>
        /// HttpGet请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="Timeout">超时时间(可选)</param>
        /// <returns></returns>
        public static async Task<string> HttpGet(string url, DecompressionMethods decompression = DecompressionMethods.None, int timeout = 10000)
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = decompression
            };

            HttpClient client = new HttpClient(handler);
            client.Timeout = TimeSpan.FromMilliseconds(timeout);
            using var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> HttpPost(string url, HttpContent content,
            Dictionary<string, string> headers = null, DecompressionMethods decompression = DecompressionMethods.None, int timeout = 10000)
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = decompression
            };

            HttpClient client = new HttpClient(handler);
            client.Timeout = TimeSpan.FromMilliseconds(timeout);
            foreach (var header in headers ?? new()) client.DefaultRequestHeaders.Add(header.Key, header.Value);
            using var response = await client.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> HttpPost(string url, string content, string content_type = "application/json", int timeout = 10000)
        {
            using var content1 = new StringContent(content);
            content1.Headers.ContentType = new MediaTypeHeaderValue(content_type);
            return await HttpPost(url, content1, timeout: timeout);
        }

        public static string ListenServer()
        {
            HttpListener listener = new HttpListener();
            string[] prefixes = new string[] { "http://localhost:12000/" }; 
            while (true)
            {
                try
                {
                    listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;//指定身份验证 Anonymous匿名访问
                    foreach (string s in prefixes)
                    {
                        listener.Prefixes.Add(s);
                    }
                    listener.Start();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }

                //线程池
                int minThreadNum;
                int portThreadNum;
                int maxThreadNum;
                ThreadPool.GetMaxThreads(out maxThreadNum, out portThreadNum);
                ThreadPool.GetMinThreads(out minThreadNum, out portThreadNum);
                while (true)
                {
                    //等待请求连接
                    //没有请求则GetContext处于阻塞状态
                    HttpListenerContext ctx = listener.GetContext();
                    string a = ServerCore(ctx);
                    Console.Write(a);
                    return a;
                }
            }
        }

        public static string ServerCore(object o)
        {
            HttpListenerContext ctx = (HttpListenerContext)o;

            ctx.Response.StatusCode = 200;//设置返回给客服端http状态代码

            //接收Get参数
            string code = ctx.Request.QueryString["code"];

            //接收POST参数
            Stream stream = ctx.Request.InputStream;
            System.IO.StreamReader reader = new System.IO.StreamReader(stream, Encoding.UTF8);
            String body = reader.ReadToEnd();

            //使用Writer输出http响应代码,UTF8格式
            using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream, Encoding.UTF8))
            {
                writer.Write(code);
                return code;
            }
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
