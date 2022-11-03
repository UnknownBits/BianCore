using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.DataType.API.Microsoft
{
    public struct RefreshTokenResponse
    {
        [JsonProperty("access_token")]
        public string Access_Token { get; set; }

        [JsonProperty("token_type")]
        public string Token_Type { get; set; }

        [JsonProperty("expires_in")]
        public int Expires { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("refresh_token")]
        public string Refresh_Token { get; set; }

        [JsonProperty("id_token")]
        public string ID_Token { get; set; }
    }
}
