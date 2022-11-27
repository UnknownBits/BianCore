using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.DataType.API.Modrinth
{
    public class ModInfo
    {
        [JsonProperty("project_id")]
        public string project_id { get; set; }

        [JsonProperty("project_type")]
        public string project_type { get; set; }

        [JsonProperty("slug")]
        public string slug { get; set; }

        [JsonProperty("author")]
        public string author { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("categories")]
        public IList<string> categories { get; set; }

        [JsonProperty("display_categories")]
        public IList<string> display_categories { get; set; }

        [JsonProperty("versions")]
        public IList<string> versions { get; set; }

        [JsonProperty("downloads")]
        public int downloads { get; set; }

        [JsonProperty("follows")]
        public int follows { get; set; }

        [JsonProperty("icon_url")]
        public string icon_url { get; set; }

        [JsonProperty("date_created")]
        public string date_created { get; set; }

        [JsonProperty("date_modified")]
        public string date_modified { get; set; }

        [JsonProperty("latest_version")]
        public string latest_version { get; set; }

        [JsonProperty("license")]
        public string license { get; set; }

        [JsonProperty("client_side")]
        public string client_side { get; set; }

        [JsonProperty("server_side")]
        public string server_side { get; set; }

        [JsonProperty("gallery")]
        public IList<string> gallery { get; set; }
    }
}
