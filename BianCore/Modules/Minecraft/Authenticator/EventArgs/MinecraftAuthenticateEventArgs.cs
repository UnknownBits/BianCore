using BianCore.DataType.Minecraft;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs {

    public class MinecraftAuthenticateEventArgs : System.EventArgs {
        public AuthenticateMinecraftResponse Response { get; set; }
    }
}
