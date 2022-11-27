using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BianCore.DataType.API.Modrinth
{
    public class SearchResponse
    {
        [JsonProperty("hits")]
        public List<ModInfo> hits { get; set; }
        [JsonProperty("offset")]
        public int offset { get; set; }
        [JsonProperty("limit")]
        public int limit { get; set; }
        [JsonProperty("total_hits")]
        public int total_hits { get; set; }
    }
}
