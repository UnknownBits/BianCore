using Newtonsoft.Json;
using System;

namespace BianCore.DataType.Minecraft.Launcher
{
    public struct VersionInfo
    {
        [JsonProperty("minecraftArguments")]
        public string MinecraftArguments { get; set; }

        [JsonProperty("arguments")]
        public ArgumentsStruct Arguments { get; set; }

        [JsonProperty("assetIndex")]
        public AssetIndexStruct AssetIndex { get; set; }

        [JsonProperty("assets")]
        public string AssetsName { get; set; }

        [JsonProperty("complianceLevel")]
        public int ComplianceLevel { get; set; }

        [JsonProperty("downloads")]
        public DownloadsStruct Downloads { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("javaVersion")]
        public JavaVersionStruct JavaVersion { get; set; }

        [JsonProperty("libraries")]
        public LibraryStruct[] Libraries { get; set; }

        [JsonProperty("logging")]
        public LoggingStruct Logging { get; set; }

        [JsonProperty("mainClass")]
        public string MainClass { get; set; }

        [JsonProperty("minimumLauncherVersion")]
        public int MinimumLauncherVersion { get; set; }

        [JsonProperty("releaseTime")]
        public DateTime ReleaseTime { get; set; }

        [JsonProperty("Time")]
        public DateTime Time { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonIgnore]
        public string VersionPath { get; set; }

        public enum TypeEnum
        {
            [JsonProperty("release")]
            Release,
            [JsonProperty("snapshot")]
            Snapshot,
            [JsonProperty("old_beta")]
            Old_Beta,
            [JsonProperty("old_alpha")]
            Old_Alpha
        }
    }
}
