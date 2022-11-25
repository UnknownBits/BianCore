using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.DataType.Minecraft.Launcher
{
    public struct AssetIndexStruct
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("sha1")]
        public string SHA1 { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("totalSize")]
        public long TotalSize { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }
    }
}
