using Newtonsoft.Json;

namespace BianCore.DataType.Minecraft.Launcher
{
    public struct JavaVersionStruct
    {
        [JsonProperty("component")]
        public string Component { get; set; }

        [JsonProperty("majorVersion")]
        public int MajorVersion { get; set; }
    }
}
