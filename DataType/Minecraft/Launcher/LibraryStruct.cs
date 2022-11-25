using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.DataType.Minecraft.Launcher
{
    public struct LibraryStruct
    {
        public struct ArtifactStruct
        {
            [JsonProperty("path")]
            public string Path { get; set; }

            [JsonProperty("sha1")]
            public string SHA1 { get; set; }

            [JsonProperty("size")]
            public long Size { get; set; }

            [JsonProperty("url")]
            public string URL { get; set; }
        }

        [JsonProperty("artifact")]
        public ArtifactStruct Artifact { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }


    }
}
