using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BianCore.Tools.API
{
    public static class Bing
    {
        public static string Link()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return new Regex("\"url\":\"(?<Url>.*?)\"", RegexOptions.IgnoreCase).Match(BianCore.Core.Config.BingBackGroud_Data.ToString()).Groups["Url"].Value.ToString();
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public static string Copyright()
        {
            try
            {
                return new Regex("\"copyright\":\"(?<Copyright>.*?)\"", RegexOptions.IgnoreCase).Match(BianCore.Core.Config.BingBackGroud_Data.ToString()).Groups["Copyright"].Value.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string Title()
        {
            try
            {
                return new Regex("\"title\":\"(?<Title>.*?)\"", RegexOptions.IgnoreCase).Match(BianCore.Core.Config.BingBackGroud_Data.ToString()).Groups["Title"].Value.ToString();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
