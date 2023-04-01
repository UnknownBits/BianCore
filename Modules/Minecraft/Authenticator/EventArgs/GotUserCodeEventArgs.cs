using BianCore.DataType.API.Microsoft;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs
{

    public class GotUserCodeEventArgs : System.EventArgs
    {
        public DeviceAuthorizationResponse Response { get; set; }
    }
}
