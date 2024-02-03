using System;
using System.Runtime.InteropServices;

namespace BianCore.Tools
{
    public static class SystemTools
    {
        private static OSPlatform? _platform;

        public enum OSPlatform { Windows, Linux, OSX }

        /// <summary>
        /// 获取系统版本
        /// </summary>
        /// <returns>系统版本字符串</returns>
        public static string GetOSVersion() { return RuntimeInformation.OSDescription; }

        public static Architecture GetArchitecture() { return RuntimeInformation.ProcessArchitecture; }


        public static void GetOSPlatform(out OSPlatform platform)
        {
            if (_platform == null)
                if (RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
                    _platform = OSPlatform.Windows;
                else if (RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
                    _platform = OSPlatform.OSX;
                else _platform = OSPlatform.Linux;
            platform = (OSPlatform)_platform;
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="model">YYYY:xxxx年 MM:xx月 DD:xx日 HH:xx时 MM:xx分 SS:xx秒</param>
        /// <returns>model格式的日期</returns>
        public static string GetTimestamp(string model) { return DateTime.Now.Date.ToString(model); }
    }
}
