using BianCore.Tools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.API
{
    public static class Modrinth
    {
        public class V2
        {
            public JToken Search()
            {
                var JsData = Tools.Json.Str_to_Json(Network.HttpGet("https://api.modrinth.com/v2/search"))["hits"];
                return JsData;
            }
        }
    }
}
