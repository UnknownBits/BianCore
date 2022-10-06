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
        /// <summary>
        /// 项目名称（动态，可更改）
        /// </summary>
        public static string Project_Name { get; set; } = "Bian_Core";

        /// <summary>
        /// 获取 App 数据根路径。（带 '\' 或 '/' 后缀）
        /// </summary>
        /// <returns>App 数据根路径。（带 '\' 或 '/' 后缀）</returns>
        public static string RootPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == true)
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + '/';
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) == true)
            {
                return @"/Applications/";
            }
            else
            {
                return @"/usr/";
            }
        }

        /// <summary>
        /// 获取该应用程序工作目录。
        /// </summary>
        /// <returns>工作目录。</returns>
        public static string WorkPath = $"{RootPath()}{Project_Name}/";
        public static string ConfigPath { set; get; } = WorkPath + "Config.bian";
        public static string Background = WorkPath + "Backgroud/";
        public static string BackgroundFile = Background + "Background.png";
        public static string Music = WorkPath + "Music/";
        public static JObject BingBackGroud_Data = Json.Str_to_Json(Network.HttpGet("https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN"));
        public static string InfoFile { get; } = $"{WorkPath}Info/{SystemTools.GetTimestamp("hh:mm:ss")}";


        internal static class Hiper
        {
            public static string WorkPath = RootPath() + "Hiper/";
            public const string Download_URL = "https://gitcode.net/to/hiper/-/raw/master/";
            public const string HashMap_URL = Download_URL + "packages.sha1";
            public static string InfoFile = WorkPath + $"Info/{DateTime.Now.Date.ToString("yyyy-MM-dd")}.Info";
        }

    }
}
