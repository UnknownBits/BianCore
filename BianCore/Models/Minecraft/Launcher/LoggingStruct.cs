using Newtonsoft.Json;

namespace BianCore.DataType.Minecraft.Launcher
{
    public struct LoggingStruct
    {
        public struct ClientStruct
        {
            [JsonProperty("argument")]
            public string Argument { get; set; }

            public struct FileStruct
            {
                [JsonProperty("id")]
                public string ID { get; set; }

                [JsonProperty("sha1")]
                public string SHA1 { get; set; }

                [JsonProperty("size")]
                public long Size { get; set; }

                [JsonProperty("url")]
                public string Url { get; set; }
            }

            [JsonProperty("file")]
            public FileStruct File { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        [JsonProperty("client")]
        public ClientStruct Client { get; set; }
    }
}
