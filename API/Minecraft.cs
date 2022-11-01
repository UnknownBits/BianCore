using BianCore.API.DataType.Minecraft;
using BianCore.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.API
{
    public static class Minecraft
    {
        public static async Task<AuthenticateMinecraftResponse> AuthenticateMinecraftRequest(string userhash, string xsts_token)
        {
            string url = "https://api.minecraftservices.com/authentication/login_with_xbox";
            string param = $"{{\"identityToken\":\"XBL3.0 x={userhash};{xsts_token}\"}}";
            using var content = new StringContent(param);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Accept", "application/json" }
            };
            string responseStr = await Network.HttpPost(url, content, headers);
            var response = JsonConvert.DeserializeObject<AuthenticateMinecraftResponse>(responseStr);
            return response;
        }
    }
}
