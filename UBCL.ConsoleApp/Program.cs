using BianCore;
using BianCore.API;
using BianCore.DataType.API.Microsoft;
using BianCore.Modules.Minecraft.Authenticator.EventArgs;
using System.Security.Cryptography;
using System.Text;

namespace UBCL.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            start();
        }
        public static void start()
        {
            var response = BianCore.API.Microsoft.OAuth.DeviceAuthorizationRequest("36103f8d-1f42-4bdc-86bc-2755738469e4").Result;
            Console.WriteLine(response.User_Code);
            Console.WriteLine(response.Verification_Uri);

            AuthorizingUserResponse userResponse;
            while (true)
            {
                Task.Delay(response.Interval * 1000);
                userResponse = BianCore.API.Microsoft.OAuth.DeviceAuthenticatingUserRequest("36103f8d-1f42-4bdc-86bc-2755738469e4", response.Device_Code).Result;
                if (userResponse.Error == null) break;
            }

            // 开始 XBL 验证
            var xblResponse = Xbox.XBLAuthenticateRequest(userResponse.Access_Token).Result;

            // 开始 XSTS 验证
            var xstsResponse = Xbox.XSTSAuthenticateRequest(xblResponse.Token).Result;

            // 获取 Minecraft 令牌
            var mcResponse = Minecraft.AuthenticateMinecraftRequest(xstsResponse.DisplayClaims.XUI[0].Uhs, xstsResponse.Token).Result;

            // 获取 Minecraft 用户档案
            var profile = Minecraft.GetProfileRequest(mcResponse.Access_Token).Result;

            Console.WriteLine(profile.UUID);
        }
    }
}