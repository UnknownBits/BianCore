using System.Runtime.InteropServices;
using System.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BianCore.Tools
{
    public static class SystemTools
    {
        /// <summary>
        /// 获取Windows系统版本
        /// </summary>
        /// <returns>Windows系统版本字符串</returns>
        public static string GetWindowsVersion()
        {
            return (string)Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("ProductName");
        }

        public static string GetOSVersion()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == true)
            {
                return "Windows";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) == true)
            {
                return "Linux";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) == true)
            {
                return "MacOS";
            }
            else return "error";
        }

        public static string GetCPUID()
        {
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            string date = null;
            foreach (ManagementObject mo in moc)
            {
                date += mo["processorid"].ToString();
            }
            return date;
        }
        public static string GetCPUName()
        {
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            string date = null;
            foreach (ManagementObject mo in moc)
            {
                date += mo["Name"].ToString();
            }
            return date;
        }
        public static string GetHardDiskID()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            string date = null;
            foreach (ManagementObject mo in searcher.Get())
            {
                date = mo["SerialNumber"].ToString().Trim();
                break;
            }
            return date;
        }
        public static string GetDisplayName()
        {
            ManagementObjectSearcher FlashDevice = new ManagementObjectSearcher("Select * from win32_VideoController");
            string date = null;
            foreach (ManagementObject FlashDeviceObject in FlashDevice.Get())
            {
                date = FlashDeviceObject["name"].ToString();
            }
            return date;
        }

    }
}
