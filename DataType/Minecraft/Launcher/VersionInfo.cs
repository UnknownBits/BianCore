using BianCore.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BianCore.DataType.Minecraft.Launcher
{
    public struct VersionInfo
    {
        [JsonProperty("arguments")]
        public ArgumentsStruct Arguments { get; set; }

        [JsonProperty("assetIndex")]
        public AssetIndexStruct AssetIndex { get; set; }

        [JsonProperty("assets")]
        public string AssetName { get; set; }

        [JsonProperty("complianceLevel")]
        public int ComplianceLevel { get; set; }

        [JsonProperty("downloads")]
        public DownloadsStruct Downloads { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("javaVersion")]
        public JavaVersionStruct JavaVersion { get; set; }
    }
}
