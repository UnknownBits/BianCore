using BianCore.DataType.Minecraft;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs;

public class GetMinecraftProfileEventArgs : System.EventArgs {
    public GetProfileResponse Response { get; set; }
}