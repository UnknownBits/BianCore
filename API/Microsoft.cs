using BianCore.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.API
{
    public static class Microsoft
    {
        public static async Task<Json.DeviceAuthorizationResponse> DeviceAuthorizationRequest(string client_id)
        {
            string url = "https://login.microsoftonline.com/consumers/oauth2/v2.0/devicecode";
            Dictionary<string, string> param = new Dictionary<string, string>()
            {
                { "client_id", client_id },
                { "scope", "XboxLive.signin offline_access" }
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(param);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            string responseStr = await Network.HttpPost(url, content);
            var response = JsonConvert.DeserializeObject<Json.DeviceAuthorizationResponse>(responseStr);
            return response;
        }

        public static async Task<Json.AuthenticatingUserResponse> AuthenticatingUserRequest(string client_id, string device_code)
        {
            string url = "https://login.microsoftonline.com/consumers/oauth2/v2.0/token";
            Dictionary<string, string> param = new Dictionary<string, string>()
            {
                { "grant_type", "urn:ietf:params:oauth:grant-type:device_code" },
                { "client_id", client_id },
                { "device_code", device_code }
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(param);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            string responseStr = await Network.HttpPost(url, content);
            var response = JsonConvert.DeserializeObject<Json.AuthenticatingUserResponse>(responseStr);
            return response;
        }
    }
}
