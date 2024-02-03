using Newtonsoft.Json;
using System;

namespace BianCore.DataType.Minecraft.Launcher
{
    public struct VersionInfo
    {
#nullable enable
        /// <summary>
        /// 1.12 及以下版本的启动参数（无 JVM 参数）
        /// </summary>
        [JsonProperty("minecraftArguments")]
        public string? MinecraftArguments { get; set; }

        /// <summary>
        /// 分离安装 Mod 加载器的继承版本
        /// </summary>
        [JsonProperty("inheritsFrom")]
        public string? InheritsFrom { get; set; }

        /// <summary>
        /// 1.13 及以上版本的启动参数
        /// </summary>
        [JsonProperty("arguments")]
        public ArgumentsStruct? Arguments { get; set; }

        [JsonProperty("assetIndex")]
        public AssetIndexStruct AssetIndex { get; set; }

        [JsonProperty("assets")]
        public string AssetsIndexName { get; set; }

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
