using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.DataType.API.Microsoft
{
    public struct AuthenticatingUserResponse
    {
        public enum ErrorEnum
        {
            [JsonProperty("authorization_pending")]
            Authorization_Pending,
            [JsonProperty("authorization_declined")]
            Authorization_Declined,
            [JsonProperty("bad_verification_code")]
            Bad_Verification_Code,
            [JsonProperty("expired_token")]
            Expired_Token,
            [JsonProperty("invalid_grant")]
            Invalid_Grant
        }

        [JsonProperty("error")]
        public ErrorEnum? Error { get; set; }

        [JsonProperty("token_type")]
        public string Token_Type { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("expires_in")]
        public int Expires { get; set; }

        [JsonProperty("access_token")]
        public string Access_Token { get; set; }

        [JsonProperty("id_token")]
        public string ID_Token { get; set; }

        [JsonProperty("refresh_token")]
        public string Refresh_Token { get; set; }
    }
}
