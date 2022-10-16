using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace BianCore.Tools.API
{
    public static class Bing
    {
        public static string Url()
        {
            try
            {
                return "https://cn.bing.com" + (string)Core.Config.BingBackGroud_Data["images"][0]["url"];

            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public static string Urlbase()
        {
            try
            {
                return "https://cn.bing.com" + (string)Core.Config.BingBackGroud_Data["images"][0]["urlbase"];
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
                return (string)Core.Config.BingBackGroud_Data["images"][0]["copyright"];
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
                return (string)Core.Config.BingBackGroud_Data["images"][0]["title"];

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
