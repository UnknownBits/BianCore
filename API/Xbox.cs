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
            string param = string.Format("{\"Properties\":{\"AuthMethod\":\"RPS\",\"SiteName\":\"user.auth.xboxlive.com\",\"RpsTicket\":\"d={0}\"},\"RelyingParty\":\"http://auth.xboxlive.com\",\"TokenType\":\"JWT\"}", access_token);
            using StringContent content = new StringContent(param);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            content.Headers.Add("Accept", "application/json");
            string responseStr = await Network.HttpPost(url, content);
            var response = JsonConvert.DeserializeObject<AuthenticationResponse>(responseStr);
            return response;
        }

        public static async Task<AuthenticationResponse> XSTSAuthenticateRequest(string xbl_token)
        {
            string url = "https://user.auth.xboxlive.com/user/authenticate";
            string param = string.Format("{\"Properties\":{\"SandboxId\":\"RETAIL\",\"UserTokens\":[\"{0}\"]},\"RelyingParty\":\"rp://api.minecraftservices.com/\",\"TokenType\":\"JWT\"}", xbl_token);
            using StringContent content = new StringContent(param);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            content.Headers.Add("Accept", "application/json");
            string responseStr = await Network.HttpPost(url, content);
            var response = JsonConvert.DeserializeObject<AuthenticationResponse>(responseStr);
            return response;
        }
    }
}
