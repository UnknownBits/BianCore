using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace BianCore.Modules.BianNetwork
{
    public class Http
    {
        public string Listen()
        {
            HttpListener listener = new HttpListener();
            string[] prefixes = new string[] { "http://localhost:8080/" };

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

        public string ServerCore(object o)
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
    }
}
