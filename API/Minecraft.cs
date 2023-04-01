using BianCore.DataType.Minecraft;
using BianCore.Tools;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BianCore.API
{
    public static class Minecraft
    {
        private static readonly Network network = new Network();
        public static async Task<AuthenticateMinecraftResponse> AuthenticateMinecraftRequest(string userhash, string xsts_token)
        {
            string url = "https://api.minecraftservices.com/authentication/login_with_xbox";
            string param = $"{{\"identityToken\":\"XBL3.0 x={userhash};{xsts_token}\"}}";
            using var httpResponse = await network.HttpPostAsync(url, param, headerPairs: new Dictionary<string, string> { { "Accept", "application/json" } });
            string responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<AuthenticateMinecraftResponse>(responseStr);
            return response;
        }

        public static async Task<GetProfileResponse> GetProfileRequest(string access_token)
        {
            string url = "https://api.minecraftservices.com/minecraft/profile";
            using var httpResponse = await network.HttpGetAsync(url, headerPairs: new Dictionary<string, string> { { "Authorization", $"Bearer {access_token}" } });
            string responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<GetProfileResponse>(responseStr);
            return response;
        }
    }
}
