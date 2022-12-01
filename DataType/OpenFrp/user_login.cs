using Newtonsoft.Json;

namespace BianCore.DataType.OpenFrp
{
    public static class user_login
    {
        public class Send
        {
            [JsonProperty("user")]
            public string user { get; set; }

            [JsonProperty("password")]
            public string password { get; set; }
        }
        public class Receive
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
