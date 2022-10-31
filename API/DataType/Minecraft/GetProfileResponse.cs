using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.API.DataType.Minecraft
{
    public struct GetProfileResponse
    {
        [JsonProperty("id")]
        public string UUID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("skin")]
    }
}
