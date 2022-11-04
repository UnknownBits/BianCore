using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Module.Minecraft
{
    public static class JavaHelper
    {
        /// <summary>
        /// 寻找在计算机中的 Java。(可能不全)
        /// </summary>
        /// <returns></returns>
        public static JavaInfo[] FindJava()
        {
            List<JavaInfo> result = new List<JavaInfo>();
            var platform = Tools.SystemTools.GetOSPlatform();
            switch (platform)
            {
                case Tools.SystemTools.OSPlatform.Windows:
                    // 在注册表中寻找 Java
                    var rootReg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem
                ? RegistryView.Registry64 : RegistryView.Registry32).OpenSubKey("SOFTWARE");
                    break;
                case Tools.SystemTools.OSPlatform.Linux:
                    break;
                case Tools.SystemTools.OSPlatform.MacOS:
                    break;
            }
            throw null;
        }
    }

    // Java 信息类
    public class JavaInfo
    {
        /// <summary>
        /// Java 版本。
        /// </summary>
        public Version JavaVersion { get; private set; }

        /// <summary>
        /// Java 路径。(java.exe)
        /// </summary>
        public string JavaPath { get; private set; }

        public JavaInfo(string version, string path)
        {
            Version javaVersion = Version.Parse(version);

            JavaVersion = javaVersion;
            JavaPath = path;
        }
    }
}
