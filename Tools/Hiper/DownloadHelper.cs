using BianCore.Core;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BianCore.Tools.Hiper
{
    public static class DownloadHelper
    {
        public static Dictionary<Architecture, string> ArchitectureMap = new Dictionary<Architecture, string>()
        {
            { Architecture.X86, "386" },
            { Architecture.X64, "amd64" },
            { Architecture.Arm, "arm-7" },
            { Architecture.Arm64, "arm64" }
        };
        public static Dictionary<string, string> OSMap = new Dictionary<string, string>()
        {
            { "Windows", "windows" },
            { "Linux", "linux" },
            { "MacOS", "darwin" }
        };
        public static Dictionary<string, string> HashMap = new Dictionary<string, string>();

        private static void GetHashMap(string hashContent)
        {
            HashMap.Clear();
            string[] hashs = hashContent.Split('\n');
            foreach (string hash in hashs)
            {
                string[] keyValue = hash.Split(' ');
                HashMap.Add(keyValue[2], keyValue[0]);
            }
        }

        /// <summary>
        /// 下载 Hiper。
        /// </summary>
        /// <param name="Architecture">系统架构。</param>
        /// <returns>Hiper 主文件路径。</returns>
        public static string DownloadHiper(Architecture Architecture)
        {
            // 获取架构，版本信息
            string OS = SystemTools.GetOSVersion();
            string Arc = ArchitectureMap[Architecture];

            // 获取哈希信息
            string HashListStr = Network.HttpGet(Config.Hiper.HashMap_URL);
            GetHashMap(HashListStr);

            // 下载 Hiper 本体并验证哈希
            if (OS == "Windows")
            {
                // 下载本体
                string remotePath = Config.Hiper.Download_URL + $"{OSMap[OS]}-{Arc}/hiper.exe";
                Downloads.Plan1(remotePath, Config.Hiper.WorkPath + "/hiper.exe");
                string hash = HashTools.GetFileSHA1(Config.Hiper.WorkPath + "/hiper.exe");
                if (hash != HashMap[remotePath])
                {
                    throw new NotImplementedException("The file hash value is incorrect.");
                }

                // 下载 WinTun
                remotePath = Config.Hiper.Download_URL + $"{OSMap[OS]}-{Arc}/wintun.dll";
                Downloads.Plan1(remotePath, Config.Hiper.WorkPath + "/wintun.dll");
                hash = HashTools.GetFileSHA1(Config.Hiper.WorkPath + "/wintun.dll");
                if (hash != HashMap[remotePath])
                {
                    throw new NotImplementedException("The file hash value is incorrect.");
                }

                return Config.WorkPath() + "hiper.exe";
            }
            else
            {
                // 下载本体
                string RemotePath = Config.Hiper.Download_URL + $"{OSMap[OS]}-{Arc}/hiper";
                Downloads.Plan1(RemotePath, Config.Hiper.WorkPath + "/hiper");
                string Hash = HashTools.GetFileSHA1(Config.Hiper.WorkPath + "/hiper");
                if (Hash != HashMap[RemotePath])
                {
                    throw new NotImplementedException("The file hash value is incorrect.");
                }

                return Config.WorkPath() + "hiper";
            }
        }

        public static void DownloadCert(string code)
        {
            string url = $"https://cert.mcer.cn/{code}.yml";
            Downloads.Plan1(url,Config.Hiper.CertsPath+$"/{code}.yml");
            Downloads.Plan1(url, Config.WorkPath() + "config.yml");
        }
    }
}