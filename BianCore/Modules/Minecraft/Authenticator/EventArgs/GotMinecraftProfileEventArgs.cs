using BianCore.DataType.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs
{

    public class GotMinecraftProfileEventArgs : System.EventArgs
    {
        public GetProfileResponse Response { get; set; }
    }
}
