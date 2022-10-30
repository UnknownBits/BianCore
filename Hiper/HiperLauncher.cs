using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.Tools.Hiper
{
    public static class HiperLauncher
    {
        public static Part Progress;
        public static StreamReader StandardOutput;
        public static StatusEnum Status = StatusEnum.Stoped;

        public static async Task Launch(string code)
        {
            Architecture architecture = SystemTools.GetArchitecture();
            Progress = DownloadHelper.Progress;
            string path = await DownloadHelper.DownloadHiper(architecture);
            Progress = Part.Downloading_Cert;
            DownloadHelper.DownloadCert(code);

            Progress = Part.Launching_Hiper;
            Process process = new Process();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;

            process.StartInfo.FileName = path;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                process.StartInfo.Verb = "runas";
            }
            process.Start();
            StandardOutput = process.StandardOutput;
            Task.Run(() =>
            {
                Status = StatusEnum.Running;
                Process process1 = Process.GetProcessById(process.Id);
                while (!process1.HasExited && !userStop) ;
                if (userStop)
                {
                    Status = StatusEnum.Stoped;
                }
                else
                {
                    Status = StatusEnum.AbnormalExit;
                    AbnormalExited(null, new EventArgs());
                }
            });
        }

        public static event EventHandler AbnormalExited;
        private static bool userStop = false;

        public static void Stop()
        {
            userStop = true;
            Process process = new Process();
            string command = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Verb = "runas";
                command = "taskkill /f /im hiper.exe";
            }
            else
            {
                process.StartInfo.FileName = "kill";
                command = "-9 hiper";
            }
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.Start();
            process.StandardInput.WriteLine(command);
            process.StandardInput.AutoFlush = true;
            process.Close();
            userStop = false;
        }

        public enum Part
        {
            Downloading_Hiper,
            Downloading_WinTun,
            Downloading_Cert,
            Launching_Hiper
        }

        public enum StatusEnum
        {
            Stoped,
            Running,
            AbnormalExit
        }
    }
}
