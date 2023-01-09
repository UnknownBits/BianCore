using BianCore.DataType.Minecraft.Launcher;
using BianCore.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BianCore.Modules.Minecraft
{
    public class Launcher
    {
        public string MinecraftPath { get; set; }

        public VersionInfo[] Versions { get; set; }

        /// <summary>
        /// Launcher 类的构造方法
        /// </summary>
        /// <param name="versionsPath">.minecraft 文件夹的路径 E.g. D:\Minecraft\.minecraft</param>
        public Launcher(string minecraftRootPath)
        {
            MinecraftPath = Path.GetFullPath(minecraftRootPath);
        }

        public static VersionInfo GetVersionInfoFromFile(string jsonPath)
        {
            using StreamReader sr = new StreamReader(jsonPath);
            VersionInfo result = JsonConvert.DeserializeObject<VersionInfo>(sr.ReadToEnd());
            result.VersionPath = Path.GetDirectoryName(jsonPath);
            return result;
        }

        public VersionInfo[] ScanVersions()
        {
            FileInfo[] infos = FileTools.SearchFile(Path.Combine(MinecraftPath, "versions"), ".json");
            List<VersionInfo> versions = new List<VersionInfo>();
            foreach (FileInfo info in infos)
            {
                if (info.Name == $"{Path.GetFileName(Path.GetDirectoryName(info.FullName))}.json")
                {
                    versions.Add(GetVersionInfoFromFile(info.FullName));
                }
            }

            Versions = versions.ToArray();
            return Versions;
        }

        private void GetJVMArguments(ref StringBuilder jvmSb, LaunchProperties prop)
        {
#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
            foreach (var arg in prop.LaunchVersion.Arguments.Value.JVM)
            {
                bool allow = true;
                if (arg.Rules != null)
                {
                    foreach (var rule in arg.Rules)
                    {
                        if (rule.OS_Name != null)
                        {
                            if (rule.IsAllow) allow = rule.OS_Name == SystemTools.GetOSPlatform().ToString().ToLower();
                            else allow = rule.OS_Name != SystemTools.GetOSPlatform().ToString().ToLower();
                            if (!allow) break;
                        }
                        if (rule.OS_Arch != null)
                        {
                            if (rule.IsAllow) allow = rule.OS_Arch == SystemTools.GetArchitecture().ToString().ToLower();
                            else allow = rule.OS_Arch == SystemTools.GetArchitecture().ToString().ToLower();
                            if (!allow) break;
                        }
                    }
                }
                if (allow)
                {
                    foreach (string value in arg.Values)
                    {
                        if (value == "-Dos.name=Windows 10")
                        {
                            jvmSb.Append(' ' + "-Dos.name=\"Windows 10\"");
                            continue;
                        }
                        jvmSb.Append(' ' + value.Replace(" ", ""));
                    }
                }
            }
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。
        }

        private void GetOldJVMArguments(ref StringBuilder jvmSb, LaunchProperties prop)
        {
            if (SystemTools.GetOSPlatform() == SystemTools.OSPlatform.Windows)
            {
                jvmSb.Append(" -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump");
            }
            else if (SystemTools.GetOSPlatform() == SystemTools.OSPlatform.OSX)
            {
                jvmSb.Append(" -XstartOnFirstThread");
                jvmSb.Append(" -Xss1M");
            }
            jvmSb.Append(" -Djava.library.path=${natives_directory}");
            jvmSb.Append(" -Dminecraft.launcher.brand=${launcher_name}");
            jvmSb.Append(" -Dminecraft.launcher.version=${launcher_version}");
            jvmSb.Append(" -cp ${classpath}");
        }

        public string BuildLaunchScript(LaunchProperties prop)
        {
            // JVM 参数
            StringBuilder jvmSb = new StringBuilder();

            // 优化参数
            jvmSb.Append($" -XX:+Use{prop.JVMProperties.GCType}");
            jvmSb.Append($" -XX:{(prop.JVMProperties.UseAdaptiveSizePolicy
                ? '+' : '-')}UseAdaptiveSizePolicy");
            jvmSb.Append($" -XX:{(prop.JVMProperties.OmitStackTraceInFastThrow
                ? '+' : '-')}OmitStackTraceInFastThrow");
            jvmSb.Append($" -Dfml.ignoreInvalidMinecraftCertificates={prop.JVMProperties.FML_IgnoreInvalidMinecraftCertificates}");
            jvmSb.Append($" -Dfml.ignorePatchDiscrepancies={prop.JVMProperties.FML_IgnorePatchDiscrepancies}");
            jvmSb.Append($" -Dlog4j2.formatMsgNoLookups=true"); // log4j CVE-2021-44228

            // 内存参数
            jvmSb.Append($" -Xmn{prop.JVMProperties.NewGenHeapSize}M");
            jvmSb.Append($" -Xmx{prop.JVMProperties.MaxHeapSize}M");

            // JVM 参数
            if (prop.LaunchVersion.Arguments.HasValue) // 1.13 及以上版本参数
            {
                GetJVMArguments(ref jvmSb, prop);
            }
            else // 1.12 及以下版本参数
            {
                GetOldJVMArguments(ref jvmSb, prop);
            }

            // 替换 JVM 参数填充符
            jvmSb.Replace("${launcher_name}", '\"' + Config.Project_Name + '\"');
            var libs = GetLibraries(prop.LaunchVersion);
            var libStrs = LibrariesToPaths(libs).ToList();
            VersionInfo inheritsVer = Launcher.GetVersionInfoFromFile(Path.Combine(
                MinecraftPath, "versions", prop.LaunchVersion.InheritsFrom, $"{prop.LaunchVersion.InheritsFrom}.json"));
            if (string.IsNullOrEmpty(prop.LaunchVersion.InheritsFrom))
            {
                libStrs.Add(Path.Combine(prop.LaunchVersion.VersionPath, $"{prop.LaunchVersion.ID}.jar"));
                jvmSb.Replace("${natives_directory}"
                    , '\"' + Path.Combine(prop.LaunchVersion.VersionPath, $"{prop.LaunchVersion.ID}-natives") + '\"');
            }
            else // 继承版本
            {
                libStrs.Add(Path.Combine(inheritsVer.VersionPath, $"{inheritsVer.ID}.jar"));
                jvmSb.Replace("${natives_directory}"
                    , '\"' + Path.Combine(inheritsVer.VersionPath, $"{inheritsVer.ID}-natives") + '\"');
            }
            jvmSb.Replace("${classpath}", '\"'
                + string.Join(Path.PathSeparator.ToString()
                , libStrs) + '\"');
            jvmSb.Append(' ' + prop.LaunchVersion.MainClass);
            jvmSb.Replace("${library_directory}", '\"' + Path.Combine(MinecraftPath, "libraries") + '\"');
            jvmSb.Replace("${classpath_separator}", Path.PathSeparator.ToString());
            jvmSb.Replace("${version_name}", '\"' + prop.LaunchVersion.ID + '\"');

            // 游戏参数
            StringBuilder gameSb = new StringBuilder();
            if (prop.LaunchVersion.Arguments.HasValue)
            {
                foreach (var arg in prop.LaunchVersion.Arguments.Value.Game)
                {
                    foreach (string value in arg.Values)
                    {
                        gameSb.Append(' ' + value);
                    }
                }
            }
            else
            {
                gameSb.Append(' ' + prop.LaunchVersion.MinecraftArguments);
            }
            gameSb.Append($" --width {prop.GameProperties.WindowWidth}");
            gameSb.Append($" --height {prop.GameProperties.WindowHeight}");

            // 替换游戏参数填充符
            bool hasOptifine = false;
            if (string.IsNullOrEmpty(prop.GameProperties.VersionType))
                gameSb.Replace(" --versionType ${version_type}", "");
            if (gameSb.ToString().Contains(" --tweakClass optifine.OptiFineForgeTweaker"))
            {
                gameSb.Replace(" --tweakClass optifine.OptiFineForgeTweaker", "");
                hasOptifine = true;
            }

            gameSb.Replace("${auth_player_name}", prop.GameProperties.Username);
            gameSb.Replace("${version_name}", '\"' + prop.LaunchVersion.ID + '\"');
            gameSb.Replace("${game_directory}", '\"' + prop.LaunchVersion.VersionPath + '\"');
            gameSb.Replace("${assets_root}", '\"' + Path.Combine(MinecraftPath, "assets") + '\"');
            if (string.IsNullOrEmpty(prop.LaunchVersion.InheritsFrom))
                gameSb.Replace("${assets_index_name}", prop.LaunchVersion.AssetsIndexName);
            else // 继承版本
                gameSb.Replace("${assets_index_name}", inheritsVer.AssetsIndexName);
            gameSb.Replace("${auth_uuid}", prop.GameProperties.UUID.Replace("-", ""));
            gameSb.Replace("${auth_access_token}", prop.GameProperties.AccessToken);
            gameSb.Replace("${user_type}", prop.GameProperties.UserType.ToString());
            gameSb.Replace("${version_type}", '\"' + prop.GameProperties.VersionType + '\"');

            // 加入 Optifine 参数
            if (hasOptifine)
            {
                gameSb.Append(" --tweakClass optifine.OptiFineForgeTweaker");
            }

            return jvmSb.Append(gameSb.ToString()).ToString().Trim();
        }

        public LibraryStruct[] GetLibraries(VersionInfo ver)
        {
            List<LibraryStruct> libs = new List<LibraryStruct>();
            List<string> libNames = new List<string>(); // 判断是否重复
            Dictionary<string, string> hashMap = new Dictionary<string, string>();
            List<LibraryStruct> todoLibs = new List<LibraryStruct>(); // 需在最后加入的 Lib

            void IterateLibraries(VersionInfo version)
            {
                foreach (var lib in version.Libraries)
                {
                    bool allow = true;
                    if (lib.Rules != null)
                    {
                        foreach (var rule in lib.Rules)
                        {
                            if (rule.OSName != null)
                            {
                                if (rule.IsAllow) allow = rule.OSName == SystemTools.GetOSPlatform().ToString().ToLower();
                                else allow = rule.OSName != SystemTools.GetOSPlatform().ToString().ToLower();
                                if (!allow) break;
                            }
                        }
                    }

                    if (allow && !libs.Contains(lib)) // 判断是否重复
                    {
                        string libVer = Regex.Match(lib.Name, @"[0-9]\.[0-9]\.?[0-9]?").Value;
                        string libName;
                        if (lib.Name.IndexOf(libVer) >= 1)
                        {
                            libName = lib.Name.Remove(lib.Name.IndexOf(libVer) - 1);
                        }
                        else libName = lib.Name;
                        if (hashMap.ContainsKey(libName) && Version.Parse(libVer)
                            > Version.Parse(hashMap[libName]))
                        {
                            libs[libNames.IndexOf(libName)] = lib;
                            continue;
                        }
                        if (libName.Contains("optifine"))
                        {
                            todoLibs.Add(lib);
                            continue;
                        }
                        libs.Add(lib);
                        libNames.Add(libName);
                        hashMap.Add(libName, libVer);
                    }
                }
            }

            // 继承版本的 Libraries
            string inheritsVerPath = Path.Combine(MinecraftPath, "versions", ver.InheritsFrom, $"{ver.InheritsFrom}.json");
            VersionInfo inheritsVer = Launcher.GetVersionInfoFromFile(inheritsVerPath);
            IterateLibraries(inheritsVer);

            // 此版本的 Libraries
            IterateLibraries(ver);
            foreach (var lib in todoLibs)
            {
                libs.Add(lib);
            }

            return libs.ToArray();
        }

        public string[] LibrariesToPaths(LibraryStruct[] libs)
        {
            List<string> result = new List<string>();
            foreach (var lib in libs)
            {
                if (lib.Download?.Artifact.Path != null) // 直接获取路径
                {
                    result.Add(Path.Combine(MinecraftPath, "libraries", lib.Download.Value
                        .Artifact.Path.Replace('/', Path.DirectorySeparatorChar)));
                    continue;
                }

                // 通过 Lib 名称获取路径（可能不准确）
                string[] name = lib.Name.Split(':');
                name[0] = name[0].Replace('.', Path.DirectorySeparatorChar);
                string path = Path.Combine(MinecraftPath, "libraries"
                    , string.Join(Path.DirectorySeparatorChar.ToString(), name)
                    , $"{name[name.Length - 2]}-{name.Last()}.jar");
                if (!result.Contains(path)) result.Add(path);
            }

            return result.ToArray();
        }
    }
}
