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
        public static string HttpGet(string url, int Timeout = 120000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "application/json";
            request.UserAgent = null;
            request.Timeout = Timeout;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream myResponseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8")))
                    {
                        string retString = myStreamReader.ReadToEnd();
                        return retString;
                    }
                }
            }
        }

        public static string HttpPost(string url, object content, string content_type = "application/json", WebHeaderCollection collection = null, int timeout = 120000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = content_type;
            request.UserAgent = null;
            request.Timeout = timeout;
            request.Headers = collection;
            using (Stream stream = request.GetRequestStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(content);
                }
            }
            request.Headers = collection;
            request.Timeout = timeout;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream myResponseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8")))
                    {
                        string retString = myStreamReader.ReadToEnd();
                        return retString;
                    }
                }
            }
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
