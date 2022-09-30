using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BianCore.Tools
{
    public static class BackGround
    {
        public static class Bing
        {
            public static string Link()
            {
                return new Regex("\"url\":\"(?<Url>.*?)\"", RegexOptions.IgnoreCase).Match(BianCore.Core.Config.BingBackGroud_Data.ToString()).Groups["Url"].Value.ToString();
            }


            public static string Title()
            {
                return new Regex("\"title\":\"(?<title>.*?)\"", RegexOptions.IgnoreCase).Match(BianCore.Core.Config.BingBackGroud_Data.ToString()).Groups["title"].Value.ToString();
            }
        }
    }
}
