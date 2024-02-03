using BianCore.DataType.API.Microsoft;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs;

public class GetUserCodeEventArgs : System.EventArgs {
    public DeviceAuthorizationResponse Response { get; set; }
}