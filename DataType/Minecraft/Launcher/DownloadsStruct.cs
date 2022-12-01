using Newtonsoft.Json;

namespace BianCore.DataType.Minecraft.Launcher
{
    public struct DownloadsStruct
    {
        [JsonProperty("client")]
        public DownloadContentStruct Client { get; set; }

        [JsonProperty("client_mappings")]
        public DownloadContentStruct Client_Mappings { get; set; }

        [JsonProperty("server")]
        public DownloadContentStruct Server { get; set; }

        [JsonProperty("server_mappings")]
        public DownloadContentStruct Server_Mappings { get; set; }
    }

    public struct DownloadContentStruct
    {
        [JsonProperty("sha1")]
        public string SHA1 { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }
    }
}
