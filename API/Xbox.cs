using BianCore.API.DataType.Xbox;
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
    public static class Xbox
    {
        public static async Task<AuthenticationResponse> XBLAuthenticateRequest(string access_token)
        {
            string url = "https://user.auth.xboxlive.com/user/authenticate";
            string param = "{\"Properties\":{\"AuthMethod\":\"RPS\",\"SiteName\":\"user.auth.xboxlive.com\",\"RpsTicket\":\"d=" + access_token + "\"},\"RelyingParty\":\"http://auth.xboxlive.com\",\"TokenType\":\"JWT\"}";
            using StringContent content = new StringContent(param);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Accept", "application/json" }
            };
            string responseStr = await Network.HttpPost(url, content, headers);
            var response = JsonConvert.DeserializeObject<AuthenticationResponse>(responseStr);
            return response;
        }

        public static async Task<AuthenticationResponse> XSTSAuthenticateRequest(string xbl_token)
        {
            string url = "https://xsts.auth.xboxlive.com/xsts/authorize";
            string param = "{\"Properties\":{\"SandboxId\":\"RETAIL\",\"UserTokens\":[\"" + xbl_token + "\"]},\"RelyingParty\":\"rp://api.minecraftservices.com/\",\"TokenType\":\"JWT\"}";
            using StringContent content = new StringContent(param);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Accept", "application/json" }
            };
            string responseStr = await Network.HttpPost(url, content, headers);
            var response = JsonConvert.DeserializeObject<AuthenticationResponse>(responseStr);
            return response;
        }
    }
}
