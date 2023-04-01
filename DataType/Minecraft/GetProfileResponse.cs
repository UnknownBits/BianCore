using Newtonsoft.Json;

namespace BianCore.DataType.Minecraft
{
    public struct GetProfileResponse
    {
        [JsonProperty("id")]
        public string UUID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("skins")]
        public SkinStruct[] Skins { get; set; }

        [JsonProperty("capes")]
        public SkinStruct[] Capes { get; set; }
    }

    public struct SkinStruct
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("state")]
        public StateType State { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("variant")]
        public VariantType? Variant { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }
    }

    public enum StateType
    {
        [JsonProperty("ACTIVE")]
        Active,
        [JsonProperty("INACTIVE")]
        Inactive
    }

    public enum VariantType
    {
        [JsonProperty("CLASSIC")]
        Classic,
        [JsonProperty("SLIM")]
        Slim
    }
}
