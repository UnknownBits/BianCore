using BianCore.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
                { "scope", "user.read" }
            };
            FormUrlEncodedContent form = new FormUrlEncodedContent(param);
            string responseStr = Network.HttpPost(url, await form.ReadAsStringAsync(), "application/x-www-form-urlencoded");
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
            FormUrlEncodedContent form = new FormUrlEncodedContent(param);
            string responseStr = Network.HttpPost(url, await form.ReadAsStringAsync(), "application/x-www-form-urlencoded");
            JObject jObject = Tools.Json.Str_to_Json(responseStr);
            if (jObject["error"] != null)
            {
                throw new NotImplementedException(
                    $"API 返回错误: {jObject["error_description"].ToString().Replace("\r\n", Environment.NewLine)}");
            }
            var response = JsonConvert.DeserializeObject<Json.AuthenticatingUserResponse>(responseStr);
            return response;
        }
    }
}
