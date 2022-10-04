using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.Tools.Hiper
{
    public static class DownloadHelper
    {
        public const string HIPER_DOWNLOAD_URL = "https://gitcode.net/to/hiper/-/raw/master/";
        public const string HIPER_PACKAGES_URL = HIPER_DOWNLOAD_URL + "packages.sha1";
        public static Dictionary<Architecture, string> ArchitectureMap = new Dictionary<Architecture, string>()
        {
            { Architecture.X86, "386" },
            { Architecture.X64, "amd64" },
            { Architecture.Arm, "arm-7" },
            { Architecture.Arm64, "arm64" }
        };
        public static Dictionary<string, string> OSMap = new Dictionary<string, string>()
        {
            { "Windows", "windows" },
            { "Linux", "linux" },
            { "MacOS", "darwin" }
        };

        public static async Task DownloadHiper(Architecture architecture)
        {
            string os = SystemTools.GetOSVersion();
            

        }
    }
}
