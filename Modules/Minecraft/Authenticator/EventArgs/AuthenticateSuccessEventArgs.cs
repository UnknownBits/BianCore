using BianCore.DataType.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs
{
    public class AuthenticateSuccessEventArgs : System.EventArgs
    {
        public string Access_Token { get; set; }
        public GetProfileResponse Profile { get; set; }
    }
}
