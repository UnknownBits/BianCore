using BianCore.DataType.API.Microsoft;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Modules.Minecraft.Authenticator.EventArgs
{

    public class DeviceAuthorizationCompletedEventArgs : System.EventArgs
    {
        public bool Success { get; set; }
        public AuthorizingUserResponse.ErrorEnum? Error { get; set; }
    }
}
