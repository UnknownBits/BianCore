using DaanV2.UUID;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Tools
{
    public static class MinecraftTools
    {
        public static string GetOfflinePlayerUUID(string username) => UUIDFactory.CreateUUID(3, 1, $"OfflinePlayer:{username}");
    }
}
