using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.DataType.API.Xbox
{
    public struct AuthenticationResponse
    {
        public struct DisplayClaimsClass
        {
            public struct XUIClass
            {
                [JsonProperty("uhs")]
                public string Uhs { get; set; }
            }

            [JsonProperty("xui")]
            public XUIClass[] XUI { get; set; }
        }

        public int XErr { get; set; }

        public string IssueInstant { get; set; }
        public string NotAfter { get; set; }
        public string Token { get; set; }

        public DisplayClaimsClass DisplayClaims { get; set; }
    }
}
