using BianCore.Tools;
using Newtonsoft.Json.Linq;
using System;

namespace BianCore.API
{
    public class Bing : IDisposable
    {
        private Network network = new Network();
        private bool disposedValue;

        public string Url;
        public string UrlBase;
        public string Copyright;
        public string Title;

        public Bing()
        {
            var data = network.HttpGet("https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN").Content.ReadAsStringAsync().Result;
            var JT = JObject.Parse(data.ToString())["images"] ?? throw new NullReferenceException();
            var image = JT[0] ?? throw new NullReferenceException();

            Url = "https://cn.bing.com" + (string)image["url"];
            UrlBase = "https://cn.bing.com" + (string)image["urlbase"];
            Copyright = (string)image["copyright"];
            Title = (string)image["title"];
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    network.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
