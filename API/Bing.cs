using BianCore.Tools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BianCore.API
{
    public class Bing : IDisposable
    {
        private Network network = new Network();
        private JObject BackGround_Data;
        private bool disposedValue;

        public Bing()
        {
            BackGround_Data = Json.Str_to_Json(network.HttpGet("https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN").Content.ReadAsStringAsync().Result.ToString());
        }
        public string Url ()
        {
            try
            {
                return "https://cn.bing.com" + (string)BackGround_Data["images"][0]["url"];

            }
            catch (Exception ex)
            {
                Config.Log.WriteLine(Log.Level.ERROR, "Bing.Url", ex.ToString());
                return null;
            }

        }
        public string Urlbase()
        {
            try
            {
                return "https://cn.bing.com" + (string)BackGround_Data["images"][0]["urlbase"];
            } 
            catch (Exception ex)
            {
                Config.Log.WriteLine(Log.Level.ERROR, "Bing.Urlbase", ex.ToString());
                return null;
            }
        }
        public string Copyright()
        {
            try
            {
                return (string)BackGround_Data["images"][0]["copyright"];
            }
            catch (Exception ex)
            {
                Config.Log.WriteLine(Log.Level.ERROR, "Bing.Copyright", ex.ToString());
                return null;
            }
        }

        public string Title()
        {
            try
            {
                return (string)BackGround_Data["images"][0]["title"];

            }
            catch (Exception ex)
            {
                Config.Log.WriteLine(Log.Level.ERROR, "Bing.Title", ex.ToString());
                return null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~Bing()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
