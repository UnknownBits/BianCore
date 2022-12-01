using BianCore.DataType.API.Microsoft;
using BianCore.Tools;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BianCore.API
{
    public static class Microsoft
    {
        public static class OAuth
        {
            private static Network network = new Network();
            public static async Task<DeviceAuthorizationResponse> DeviceAuthorizationRequest(string client_id)
            {
                string url = "https://login.microsoftonline.com/consumers/oauth2/v2.0/devicecode";
                Dictionary<string, string> param = new Dictionary<string, string>()
                {
                    { "client_id", client_id },
                    { "scope", "XboxLive.signin offline_access" }
                };
                using var content = new FormUrlEncodedContent(param);
                using var httpResponse = await network.HttpPostAsync(url, content);
                string responseStr = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<DeviceAuthorizationResponse>(responseStr);
                return response;
            }

            public static async Task<AuthenticatingUserResponse> DeviceAuthenticatingUserRequest(string client_id, string device_code)
            {
                string url = "https://login.microsoftonline.com/consumers/oauth2/v2.0/token";
                Dictionary<string, string> param = new Dictionary<string, string>()
                {
                    { "grant_type", "urn:ietf:params:oauth:grant-type:device_code" },
                    { "client_id", client_id },
                    { "device_code", device_code }
                };
                using var content = new FormUrlEncodedContent(param);
                using var httpResponse = await network.HttpPostAsync(url, content);
                string responseStr = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<AuthenticatingUserResponse>(responseStr);
                return response;
            }

            public static async Task<RefreshTokenResponse> RefreshTokenRequest(string refresh_token)
            {
                string url = "https://login.microsoftonline.com/consumers/oauth2/v2.0/token";
                Dictionary<string, string> param = new Dictionary<string, string>()
                {
                    { "client_id", "00000000402b5328" },
                    { "refresh_token", refresh_token },
                    { "grant_type", "refresh_token" }
                };
                using var content = new FormUrlEncodedContent(param);
                using var httpResponse = await network.HttpPostAsync(url, content);
                string responseStr = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<RefreshTokenResponse>(responseStr);
                return response;
            }
        }
    }
}
