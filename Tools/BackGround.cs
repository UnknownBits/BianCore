using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BianCore.Tools
{
    public static class BackGround
    {


        public static class Bing_BackGround
        {
            public static string Link()
            {
                Regex regex = new Regex("\"url\":\"(?<Url>.*?)\"", RegexOptions.IgnoreCase);
                return "https://cn.bing.com" + regex.Matches(Core.Config.BingBackGroud_Data.ToString())[0].Groups["Url"].Value.ToString();
            }
            public static string Title()
            {
                Regex regex = new Regex("\"title\":\"(?<title>.*?)\"", RegexOptions.IgnoreCase);
                return regex.Matches(Core.Config.BingBackGroud_Data.ToString())[0].Groups["title"].Value.ToString();
            }
        }
    }
}
