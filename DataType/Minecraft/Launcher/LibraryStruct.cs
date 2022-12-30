using BianCore.API;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

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

        public struct ClassifierStruct
        {
            public string Path { get; set; }

            public string SHA1 { get; set; }

            public long Size { get; set; }

            public string Url { get; set; }
        }

        [JsonProperty("classifiers")]
        public Dictionary<string, ClassifierStruct> Classifiers { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public struct NativesStruct
        {
            [JsonProperty("windows")]
            public string Windows { get; set; }

            [JsonProperty("linux")]
            public string Linux { get; set; }

            [JsonProperty("osx")]
            public string MacOS { get; set; }
        }

        public struct RuleStruct
        {
            [JsonIgnore]
            public bool IsAllow { get; set; }

#nullable enable
            [JsonIgnore]
            public string? OSName { get; set; }

            private string action;

            private JToken os;

            [OnDeserialized]
            public void OnDeserialized(StreamingContext context)
            {
                IsAllow = action == "allow" || action == null;
                OSName = os?["name"]?.ToString();
            }
        }

        [JsonProperty("rules")]
        public RuleStruct[] Rules { get; set; }
    }
}
