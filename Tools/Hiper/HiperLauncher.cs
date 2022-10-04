using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace BianCore.Tools.Hiper
{
    public static class HiperLauncher
    {
        public static void Launch(string code)
        {
            Architecture architecture = SystemTools.GetArchitecture();
            string content = DownloadHelper.DownloadHiper(architecture);
            DownloadHelper.DownloadCert(code);
            Process.Start(content);
        }
    }
}
