using BianCore.DataType.Minecraft.Launcher;
using BianCore.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SeaMinecraftLauncherCore.Tools
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

            switch (SystemTools.GetOSPlatform())
            {
                case SystemTools.OSPlatform.Windows:
                    var rootReg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem
                        ? RegistryView.Registry64 : RegistryView.Registry32).OpenSubKey("SOFTWARE");
                    if (rootReg == null)
                    {
                        return new JavaInfo[0];
                    }


                    RegistryKey javaRootReg;
                    try
                    {
                        javaRootReg = rootReg.OpenSubKey("JavaSoft");
                    }
                    catch
                    {
                        return new JavaInfo[0];
                    }
                    try
                    {
                        // 在注册表中寻找 Java
                        var jreRootReg = javaRootReg.OpenSubKey("Java Runtime Environment");
                        if (jreRootReg != null)
                        {
                            foreach (string jre in jreRootReg.GetSubKeyNames())
                            {
                                try
                                {
                                    var jreInfo = jreRootReg.OpenSubKey(jre);
                                    string path = Path.Combine(jreInfo.GetValue("JavaHome").ToString(), "bin\\java.exe");
                                    string version = FileVersionInfo.GetVersionInfo(path).ProductVersion;
                                    javaList.Add(new JavaInfo(version, path));
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }

                    try
                    {
                        var jdkRootReg = javaRootReg.OpenSubKey("JDK");
                        if (jdkRootReg != null)
                        {
                            foreach (string jdk in jdkRootReg.GetSubKeyNames())
                            {
                                var jdkInfo = jdkRootReg.OpenSubKey(jdk);
                                string path = Path.Combine(jdkInfo.GetValue("JavaHome").ToString(), "bin\\java.exe");
                                string version = FileVersionInfo.GetVersionInfo(path).ProductVersion;
                                javaList.Add(new JavaInfo(version, path));
                            }
                        }
                    }
                    catch { }
                    break;
                case SystemTools.OSPlatform.Linux:
                    throw new NotImplementedException("暂不支持寻找 Linux Java。");
                    break;
                case SystemTools.OSPlatform.MacOS:
                    throw new NotImplementedException("暂不支持寻找 MacOS Java。");
                    break;
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
            Version version = Version.Parse(versionInfo.AssetName);
            if (version.Minor <= 16 && version.Major == 1)
            {
                dstJavaMajor = 8;
            }
            else if (version.Minor == 17 && version.Major == 1)
            {
                dstJavaMajor = 16;
            }
            else if (version.Minor >= 18 && version.Major == 1)
            {
                dstJavaMajor = 17;
            }
            else
            {
                if (versionInfo.JavaVersion.MajorVersion != 0)
                {
                    dstJavaMajor = versionInfo.JavaVersion.MajorVersion;
                }
                else
                {
                    throw new NotImplementedException("不支持此版本的自动选择 Java。");
                }
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

    public class JavaInfo
    {
        public readonly Version JavaVersion;
        public readonly string JavaPath;

        internal JavaInfo(string version, string javaHome)
        {
            Version javaVersion = Version.Parse(version);
            JavaVersion = javaVersion;
            JavaPath = javaHome;
        }

        public override bool Equals(object obj)
        {
            JavaInfo javaInfo = obj as JavaInfo;
            return this == javaInfo;
        }

        public static bool operator ==(JavaInfo java1, JavaInfo java2)
        {
            return java1?.JavaPath == java2?.JavaPath;
        }

        public static bool operator !=(JavaInfo java1, JavaInfo java2)
        {
            return !(java1 == java2);
        }
    }
}
