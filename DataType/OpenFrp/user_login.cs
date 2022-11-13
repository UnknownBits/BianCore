using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.DataType.OpenFrp
{
    public static class user_login
    {
        public class send
        {
            [JsonProperty("user")]
            public string user { get; set; }

            [JsonProperty("password")]
            public string password { get; set; }
        }
        public class receive
        {
            [JsonProperty("data")]
            public string data { get; set; }

            [JsonProperty("flag")]
            public bool flag { get; set; }

            [JsonProperty("msg")]
            public string msg { get; set; }
        }
    }
}
