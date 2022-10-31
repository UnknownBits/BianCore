using BianCore.Tools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BianCore.API
{
    public static class Bing
    {
        public static JObject BackGround_Data = Json.Str_to_Json(Network.HttpGet("https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN").Result);
        public static string Url()
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
        public static string Urlbase()
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
        public static string Copyright()
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

        public static string Title()
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
