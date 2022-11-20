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
        public struct ArgumentsStruct
        {
            [JsonIgnore]
            public string[] Game { get; set; }

            [JsonIgnore]
            public string[] JVM { get; set; }

            private JToken game;

            private JToken jvm;

            [OnDeserialized]
            private void OnDeserialized()
            {
                List<string> strings = new List<string>();
                foreach (var item in game)
                {
                    if (item.Type == JTokenType.String)
                    {
                        strings.Add(item.ToString());
                    }
                    else
                    {
                        bool isAllow = (string)item["$.rules[0].action"] == "allow";
                    }
                }
            }
        }
    }
}
