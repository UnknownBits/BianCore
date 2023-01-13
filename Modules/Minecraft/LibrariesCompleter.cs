using BianCore.DataType.Minecraft.Launcher;
using BianCore.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BianCore.Modules.Minecraft
{
    public class LibrariesCompleter
    {
        public string MinecraftPath { get; set; }

        public LibrariesCompleter(string path)
        {
            MinecraftPath = Path.GetFullPath(path);
        }

        public static LibraryStruct[] GetLibraries(VersionInfo ver)
        {
            string minecraftPath = SystemTools.GetMinecraftRootPath(ver.VersionPath);
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
            if (ver.InheritsFrom != null)
            {
                string inheritsVerPath = Path.Combine(minecraftPath, "versions", ver.InheritsFrom, $"{ver.InheritsFrom}.json");
                VersionInfo inheritsVer = Launcher.GetVersionInfoFromFile(inheritsVerPath);
                IterateLibraries(inheritsVer);
            }

            // 此版本的 Libraries
            IterateLibraries(ver);
            foreach (var lib in todoLibs)
            {
                libs.Add(lib);
            }

            return libs.ToArray();
        }

        public static string[] LibrariesToPaths(LibraryStruct[] libs, string minecraftPath)
        {
            List<string> result = new List<string>();
            foreach (var lib in libs)
            {
                result.Add(LibraryToPath(lib, minecraftPath));
            }

            return result.ToArray();
        }

        public static string LibraryToPath(LibraryStruct lib, string minecraftPath)
        {
            if (lib.Download?.Artifact.Path != null) // 直接获取路径
            {
                return Path.Combine(minecraftPath, "libraries", lib.Download.Value
                    .Artifact.Path.Replace('/', Path.DirectorySeparatorChar));
            }

            // 通过 Lib 名称获取路径（可能不准确）
            string[] name = lib.Name.Split(':');
            name[0] = name[0].Replace('.', Path.DirectorySeparatorChar);
            string path = Path.Combine(minecraftPath, "libraries"
                , string.Join(Path.DirectorySeparatorChar.ToString(), name)
                , $"{name[name.Length - 2]}-{name.Last()}.jar");

            return path;
        }

        public static LibraryStruct[] GetMissingLibraries(LibraryStruct[] libs, string minecraftPath, bool validHash = true)
        {
            List<LibraryStruct> result = new List<LibraryStruct>();
            foreach (var lib in libs)
            {
                string path = LibraryToPath(lib, minecraftPath);
                if (lib.Download?.Artifact != null)
                {
                    if (File.Exists(path) && (!validHash
                        || Encryption.GetFileSHA1(path) == lib.Download.Value.Artifact.SHA1))
                    {
                        result.Add(lib);
                    }
                }
                else
                {
                    if (File.Exists(path))
                        result.Add(lib);
                }
            }
            
            return result.ToArray();
        }
    }
}
