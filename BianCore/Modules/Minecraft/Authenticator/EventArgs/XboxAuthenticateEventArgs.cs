using BianCore.DataType.API.Xbox;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs;

public class XboxAuthenticateEventArgs : System.EventArgs {
    public AuthenticationResponse Response { get; set; }
}
