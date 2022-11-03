using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.DataType.API.Microsoft
{
    public struct DeviceAuthorizationResponse
    {
        [JsonProperty("device_code")]
        public string Device_Code { get; set; }

        [JsonProperty("user_code")]
        public string User_Code { get; set; }

        [JsonProperty("verification_uri")]
        public string Verification_Uri { get; set; }

        [JsonProperty("expires_in")]
        public int Expires { get; set; }

        [JsonProperty("interval")]
        public int Interval { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
