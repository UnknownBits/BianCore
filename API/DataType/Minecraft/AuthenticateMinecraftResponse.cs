using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.API.DataType.Minecraft
{
    public struct AuthenticateMinecraftResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("access_token")]
        public string Access_Token { get; set; }

        [JsonProperty("token_type")]
        public string Token_Type { get; set; }

        [JsonProperty("expires_in")]
        public int Expires { get; set; }
    }
}
