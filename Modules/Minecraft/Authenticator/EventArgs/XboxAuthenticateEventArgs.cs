using BianCore.DataType.API.Xbox;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs
{

    public class XboxAuthenticateEventArgs : System.EventArgs
    {
        public AuthenticationResponse Response { get; set; }
    }
}
