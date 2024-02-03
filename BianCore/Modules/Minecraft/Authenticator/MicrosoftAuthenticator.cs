using BianCore.API;
using BianCore.DataType.API.Microsoft;
using BianCore.Modules.Minecraft.Authenticator.EventArgs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BianCore.Modules.Minecraft.Authenticator;

public class MicrosoftAuthenticator
{
    private CancellationTokenSource CancellationToken;
    private bool _isCanceled = true;

    public string _clientId { get; set; }

    public event EventHandler<GetUserCodeEventArgs> GetUserCode = delegate { };
    public event EventHandler<DeviceAuthorizationCompletedEventArgs> DeviceAuthorizationCompleted = delegate { };
    public event EventHandler<XboxAuthenticateEventArgs> XBLAuthenticateCompleted = delegate { };
    public event EventHandler<XboxAuthenticateEventArgs> XSTSAuthenticateCompleted = delegate { };
    public event EventHandler<MinecraftAuthenticateEventArgs> MinecraftAuthenticateCompleted = delegate { };
    public event EventHandler<GetMinecraftProfileEventArgs> GetMinecraftProfile = delegate { };
    public event EventHandler<AuthenticateSuccessEventArgs> AuthenticateSuccess = delegate { };
    public event EventHandler<AuthenticateFailedEventArgs> AuthenticateFailed = delegate { };

    public MicrosoftAuthenticator(string client_id)
    {
        _clientId = client_id;
        CancellationToken = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken());
    }

    public void BeginAuthenticate()
    {
        Task.Run(async () =>
        {
            try
            {
                _isCanceled = true;

                // 设备代码流 第一步
                var response = await API.Microsoft.OAuth.DeviceAuthorizationRequest(_clientId);
                GetUserCode(null, new GetUserCodeEventArgs { Response = response });

                // 开始轮询
                AuthorizingUserResponse userResponse;
                while (true)
                {
                    await Task.Delay(response.Interval * 1000);
                    if (CancellationToken.IsCancellationRequested == true) return;
                    userResponse = await API.Microsoft.OAuth.DeviceAuthenticatingUserRequest(_clientId, response.Device_Code);
                    if (userResponse.Error != null && userResponse.Error != AuthorizingUserResponse.ErrorEnum.Authorization_Pending)
                    {
                        DeviceAuthorizationCompleted(null, new DeviceAuthorizationCompletedEventArgs { Success = false, Error = userResponse.Error });
                        _isCanceled = false;
                        return;
                    }
                    else if (userResponse.Error == null) break;
                }
                _isCanceled = false;

                // 开始 XBL 验证
                var xblResponse = await Xbox.XBLAuthenticateRequest(userResponse.Access_Token);
                XBLAuthenticateCompleted(null, new XboxAuthenticateEventArgs { Response = xblResponse });

                // 开始 XSTS 验证
                var xstsResponse = await Xbox.XSTSAuthenticateRequest(xblResponse.Token);
                XSTSAuthenticateCompleted(null, new XboxAuthenticateEventArgs { Response = xstsResponse });

                // 获取 Minecraft 令牌
                var mcResponse = await API.Minecraft.AuthenticateMinecraftRequest(xstsResponse.DisplayClaims.XUI[0].Uhs, xstsResponse.Token);
                MinecraftAuthenticateCompleted(null, new MinecraftAuthenticateEventArgs { Response = mcResponse });

                // 获取 Minecraft 用户档案
                var profile = await API.Minecraft.GetProfileRequest(mcResponse.Access_Token);
                GetMinecraftProfile(null, new GetMinecraftProfileEventArgs { Response = profile });

                AuthenticateSuccess(null, new AuthenticateSuccessEventArgs { Access_Token = mcResponse.Access_Token, Profile = profile });
            }
            catch (Exception ex)
            {
                AuthenticateFailed(null, new AuthenticateFailedEventArgs { Exception = ex });
            }
        });
    }

    public void StopAuthenticate() {
        CancellationToken.Cancel();
    }
}
