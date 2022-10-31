using BianCore.API.DataType.Minecraft;
using BianCore.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.API
{
    public static class Minecraft
    {
        public static async Task<AuthenticateMinecraftResponse> AuthenticateMinecraftRequest(string userhash, string xsts_token)
        {
            string url = "https://api.minecraftservices.com/authentication/login_with_xbox";
            string param = "{\"identityToken\":\"XBL3.0 x=" + $"{userhash};{xsts_token}" + "\"}";
            using var content = new StringContent(param);
            string responseStr = await Network.HttpPost(url, content);
            var response = JsonConvert.DeserializeObject<AuthenticateMinecraftResponse>(responseStr);
            return response;
        }
    }
}
