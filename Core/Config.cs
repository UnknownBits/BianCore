using BianCore.Tools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BianCore.Core
{
    public static class Config
    {
        public static string Project_Name { get; set; } = "Bian_Core";
        public static string WorkPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == true)
            {
                return @"%AppData%/" + Project_Name;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) == true)
            {
                return @"/Applications/" + Project_Name;
            }
            else
            {
                return @"/usr/" + Project_Name;
            }
        }
        public static string ConfigPath { set; get; } = WorkPath() + "Config.king";
        public static string Background = WorkPath() + @"\Backgroud\";
        public static string BackgroundFile = Background + "Background.png";
        public static string Music = WorkPath() + @"\Music\";
        public static JObject BingBackGroud_Data = Json.Str_to_Json(Network.HttpGet("https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN"));
        public const string Hiper_Download_URL = "https://gitcode.net/to/hiper/-/raw/master/";
        public const string Hiper_Packages_URL = Hiper_Download_URL + "packages.sha1";
    }
}
