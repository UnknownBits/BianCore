using BianCore.Tools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace BianCore
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
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
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
        public static string Work_Path { get; } = $"{RootPath()}{Project_Name}/";
        public static string Config_Path { get; } = $"{Work_Path}Config.bian";
        public static string BackGround_Path { get; } = $"{Work_Path}Backgroud/";
        public static string BackGround_File { get; set; } = Bing.BackGround_File; // 默认Bing壁纸
        public static string Music_Path { get; } = $"{Work_Path}Music/";

        public static string Log_File { get; } = $"{Work_Path}Info/{DateTime.Now.ToString("HH-mm")}.Log";
        public static Log Log = new Log(Log_File);

        public static class Bing
        {
            public static string BackGround_File { get; } = $"{BackGround_Path}Bing.jpg";
            public static string BackGround_Date { get; } = $"{BackGround_Path}Bing.date";
            public static JObject BackGround_Data = Json.Str_to_Json(Network.HttpGet("https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN"));
        }
        internal static class Hiper
        {
            public static string Work_Path = RootPath() + "Hiper/";
            public const string Download_URL = "https://gitcode.net/to/hiper/-/raw/master/";
            public const string HashMap_URL = Download_URL + "packages.sha1";
            public static string Log_File = Work_Path + $"Log/{DateTime.Now.ToString("HH-mm")}.Log";
            public static Log Log = new Log(Log_File);
        }

    }
}
