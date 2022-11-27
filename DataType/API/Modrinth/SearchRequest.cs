using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace BianCore.DataType.API.Modrinth
{
    public class SearchRequest
    {
        [JsonProperty("query")]
        public string query { get; set; }
        [JsonProperty("facets")]
        public string[] facets { get; set; }
        [JsonProperty("index")]
        public string index { get; set; }
        [JsonProperty("offset")]
        public int offset { get; set; }
        [JsonProperty("limit")]
        public int limit { get; set; }
        [JsonProperty("filters")]
        public string filters { get; set; }
    }
}
