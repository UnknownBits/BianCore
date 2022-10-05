using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.Tools.Hiper
{
    public static class HiperLauncher
    {
        public static void Launch(string code)
        {
            Architecture architecture = SystemTools.GetArchitecture();
            string path = DownloadHelper.DownloadHiper(architecture);
            DownloadHelper.DownloadCert(code);

            Process process = new Process();
            process.StartInfo.FileName = path;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                process.StartInfo.Verb = "runas";
            }
            process.Start();
        }
    }
}
