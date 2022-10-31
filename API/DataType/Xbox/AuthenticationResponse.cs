using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.API.DataType.Xbox
{
    public struct AuthenticationResponse
    {
        public string IssueInstant { get; set; }
        public string NotAfter { get; set; }
        public string Token { get; set; }

        [JsonProperty("$.DisplayClaims.xui.uhs")]
        public string Uhs { get; set; }
    }
}
