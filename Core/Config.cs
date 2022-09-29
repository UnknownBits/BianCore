using BianCore.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Core
{
    public static class Config
    {
        public static string Project_Name { get; set; } = "Bian_Core";
        public static string WorkPath = Environment.CurrentDirectory + @"\" + Project_Name + @"\";
        public static string ConfigPath = WorkPath + "Config.king";
        public static string Background = WorkPath + @"\Backgroud\";
        public static string BackgroundFile = Background + "Background.png";
        public static string Music = WorkPath + @"\Music\";
        public static string BingBackGroud_Data = Network.HttpGet("https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN");
    }
}
