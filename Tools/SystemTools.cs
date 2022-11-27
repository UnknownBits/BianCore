using System.Runtime.InteropServices;
using System.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.Tools
{
    public static class SystemTools
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="model">YYYY:xxxx年 MM:xx月 DD:xx日 HH:xx时 MM:xx分 SS:xx秒</param>
        /// <returns>model格式的日期</returns>
        public static string GetTimestamp(string model)
        {
            return DateTime.Now.Date.ToString(model);
        }

        /// <summary>
        /// 获取Windows系统版本
        /// </summary>
        /// <returns>Windows系统版本字符串</returns>
        public static string GetOSVersion()
        {
            return RuntimeInformation.OSDescription;
        }

        public static Architecture GetArchitecture()
        {
            return RuntimeInformation.ProcessArchitecture;
        }

        public static OSPlatform GetOSPlatform()
        {
            // 判断是否调用过方法，若调用过则直接返回缓存
            if (_platform != null) return (OSPlatform)_platform;
            else if (RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows) == true)
            {
                _platform = OSPlatform.Windows;
                return OSPlatform.Windows;
            }
            else if (RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX) == true)
            {
                _platform = OSPlatform.MacOS;
                return OSPlatform.MacOS;
            }
            else
            {
                _platform = OSPlatform.Linux;
                return OSPlatform.Linux;
            }
        }

        private static OSPlatform? _platform;

        public enum OSPlatform
        {
            Windows,
            Linux,
            MacOS
        }

        public static string GetCPUID()
        {
            try
            {
                if (GetOSPlatform() == OSPlatform.Windows)
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
                else return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public static string GetCPUName()
        {
            try
            {
                if (GetOSPlatform() == OSPlatform.Windows)
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
                else return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public static string GetHardDiskID()
        {
            try
            {
                if (GetOSPlatform() == OSPlatform.Windows)
                {
                    ManagementObjectSearcher FlashDevice = new ManagementObjectSearcher("Select * from win32_VideoController");
                    string date = null;
                    foreach (ManagementObject FlashDeviceObject in FlashDevice.Get())
                    {
                        date = FlashDeviceObject["name"].ToString();
                    }
                    return date;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public static string GetDisplayName()
        {
            try
            {
                if (GetOSPlatform() == OSPlatform.Windows)
                {
                    ManagementObjectSearcher FlashDevice = new ManagementObjectSearcher("Select * from win32_VideoController");
                    string date = null;
                    foreach (ManagementObject FlashDeviceObject in FlashDevice.Get())
                    {
                        date = FlashDeviceObject["name"].ToString();
                    }
                    return date;
                }
                else return null;
            }
            catch( Exception ex )
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
    }
}
