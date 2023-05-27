using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs
{

    public class AuthenticateFailedEventArgs : System.EventArgs
    {
        public Exception Exception { get; set; }
    }
}
