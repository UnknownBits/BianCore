using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.Tools.Hiper
{
    public static class HiperLauncher
    {
        public static Part Progress;
        public static StreamReader StandardOutput;

        public static void Launch(string code)
        {
            Architecture architecture = SystemTools.GetArchitecture();
            Progress = DownloadHelper.Progress;
            string path = DownloadHelper.DownloadHiper(architecture);
            Progress = Part.Downloading_Cert;
            DownloadHelper.DownloadCert(code);

            Progress = Part.Launching_Hiper;
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = path;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                process.StartInfo.Verb = "runas";
            }
            process.Start();
            StandardOutput = process.StandardOutput;
        }

        public static void Stop()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            string command = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                process.StartInfo.Verb = "runas";
                command = "taskkill /f /im hiper.exe";
            }
            else
            {
                command = "kill -9 hiper";
            }
            process.Start();
            process.StandardInput.WriteLine(command);
            process.StandardInput.AutoFlush = true;
            process.WaitForExit();
            process.Close();
        }

        public enum Part
        {
            Downloading_Hiper,
            Downloading_WinTun,
            Downloading_Cert,
            Launching_Hiper
        }
    }
}
