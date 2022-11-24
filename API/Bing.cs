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
    public class Bing
    {
        internal Network network = new Network();
        public async Task<JObject> Data()
        {
            return Json.Str_to_Json(await (await network.HttpGetAsync("https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN")).Content.ReadAsStringAsync());
        }
        internal static JObject BackGround_Data;
        public Bing()
        {
            BackGround_Data = Data().Result;
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
    }
}
