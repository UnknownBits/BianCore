using BianCore.DataType.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs
{

    public class MinecraftAuthenticateEventArgs : System.EventArgs
    {
        public AuthenticateMinecraftResponse Response { get; set; }
    }
}
