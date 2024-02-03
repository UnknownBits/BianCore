using BianCore.DataType.API.Microsoft;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs;

public class DeviceAuthorizationCompletedEventArgs : System.EventArgs {
    public bool Success { get; set; }
    public AuthorizingUserResponse.ErrorEnum? Error { get; set; }
}