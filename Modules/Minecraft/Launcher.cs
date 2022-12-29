using BianCore.DataType.Minecraft.Launcher;
using BianCore.Tools;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public Launcher(string minecraftPath)
        {
            string fullPath = Path.GetFullPath(minecraftPath);
            if (fullPath.Last() == '\\')
            {
                MinecraftPath = fullPath;
            }
            else
            {
                MinecraftPath = fullPath + '\\';
            }
        }

        public VersionInfo GetVersionInfoFromFile(string jsonPath)
        {
            using StreamReader sr = new StreamReader(jsonPath);
            VersionInfo result = JsonConvert.DeserializeObject<VersionInfo>(sr.ReadToEnd());
            result.VersionPath = Path.GetDirectoryName(jsonPath);
            return result;
        }

        public VersionInfo[] ScanVersions()
        {
            FileInfo[] infos = FileTools.SearchFile(MinecraftPath + "versions", ".json");
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

        public string GenerateLaunchScript(LaunchProperties properties)
        {
            throw new System.NotImplementedException();
        }
    }
}
