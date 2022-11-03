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
            private Network network = new Network();
            public async Task<JToken> Search()
            {
                var JsData = Tools.Json.Str_to_Json(await (await network.HttpGetAsync("https://api.modrinth.com/v2/search")).Content.ReadAsStringAsync())["hits"];
                return JsData;
            }
        }
    }
}
