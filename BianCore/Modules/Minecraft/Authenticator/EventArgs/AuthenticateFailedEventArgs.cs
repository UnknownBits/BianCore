using System;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs;

public class AuthenticateFailedEventArgs : System.EventArgs {
    public Exception Exception { get; set; }
}