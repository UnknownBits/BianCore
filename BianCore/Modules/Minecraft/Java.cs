using BianCore.DataType;
using BianCore.DataType.Minecraft.Launcher;
using BianCore.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BianCore.Modules.Minecraft
{
    public static class Java
    {
        /// <summary>
        /// 寻找计算机中的 Java。(可能不全)
        /// </summary>
        /// <returns>查找到的 Java 信息。</returns>
        public static JavaInfo[] FindJava()
        {
            List<JavaInfo> javaList = new List<JavaInfo>();
            SystemTools.GetOSPlatform(out SystemTools.OSPlatform platform);
            switch (platform)
            {
                case SystemTools.OSPlatform.Windows:
                    var rootReg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32).OpenSubKey("SOFTWARE");
                    if (rootReg == null) return Array.Empty<JavaInfo>();

                    RegistryKey javaRootReg = rootReg.OpenSubKey("JavaSoft");
                    if (javaRootReg == null) return Array.Empty<JavaInfo>();

                    // 在注册表中寻找 Java
                    var jreRootReg = javaRootReg.OpenSubKey("Java Runtime Environment");
                    if (jreRootReg != null)
                        foreach (string jre in jreRootReg.GetSubKeyNames())
                        {
                            var jreInfo = jreRootReg.OpenSubKey(jre);
                            if (jreInfo == null) continue;

                            var value = jreInfo.GetValue("JavaHome");
                            if (value == null) continue;

                            var path = Path.Combine(value.ToString(), "bin\\java.exe");
                            var version = FileVersionInfo.GetVersionInfo(path).ProductVersion;
                            javaList.Add(new JavaInfo(version, path));

                        }

                    var jdkRootReg = javaRootReg.OpenSubKey("JDK");
                    if (jdkRootReg != null)
                        foreach (string jdk in jdkRootReg.GetSubKeyNames())
                        {
                            var jdkInfo = jdkRootReg.OpenSubKey(jdk);
                            if (jdkInfo == null) continue;

                            var value = jdkInfo.GetValue("JavaHome");
                            if (value == null) continue;

                            string path = Path.Combine(value.ToString(), "bin\\java.exe");
                            string version = FileVersionInfo.GetVersionInfo(path).ProductVersion;
                            javaList.Add(new JavaInfo(version, path));

                        }
                    break;
                case SystemTools.OSPlatform.Linux:
                    throw new NotImplementedException("暂不支持寻找 Linux Java。");
                case SystemTools.OSPlatform.OSX:
                    throw new NotImplementedException("暂不支持寻找 MacOS Java。");
            }
            return javaList.ToArray();
        }

        /// <summary>
        /// 自动选择 Java。
        /// </summary>
        /// <param name="versionInfo"></param>
        /// <param name="javaInfos"></param>
        /// <returns>Java 信息。</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static JavaInfo AutoSelectJava(VersionInfo versionInfo, JavaInfo[] javaInfos)
        {
            int dstJavaMajor;
            Version version = Version.Parse(versionInfo.AssetsIndexName ?? versionInfo.InheritsFrom);
            if (versionInfo.JavaVersion.MajorVersion != 0)
            {
                dstJavaMajor = versionInfo.JavaVersion.MajorVersion;
            }
            else if (version.Minor <= 16 && version.Major == 1)
            {
                dstJavaMajor = 8;
            }
            else if (version.Minor == 17 && version.Major == 1)
            {
                dstJavaMajor = 16;
            }
            else if (version.Minor >= 18 && version.Major >= 1)
            {
                dstJavaMajor = 17;
            }
            else
            {
                throw new NotImplementedException("不支持此版本的自动选择 Java。");
            }
            JavaInfo selectJava = null;
            foreach (var javaInfo in javaInfos)
            {
                if (selectJava != null)
                {
                    if (javaInfo.JavaVersion > selectJava.JavaVersion && javaInfo.JavaVersion.Major == dstJavaMajor)
                    {
                        selectJava = javaInfo;
                    }
                }
                else if (javaInfo.JavaVersion.Major == dstJavaMajor)
                {
                    selectJava = javaInfo;
                }
            }
            if (selectJava == null)
            {
                throw new ArgumentException($"不存在 Major 版本为 {dstJavaMajor} 的 Java。");
            }
            return selectJava;
        }
    }
}
