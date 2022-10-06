using BianCore.Core;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="architecture">系统架构。</param>
        /// <param name="vaildHash">是否验证哈希。</param>
        /// <returns>Hiper 主文件路径。</returns>
        public static string DownloadHiper(Architecture architecture, bool vaildHash = true)
        {
            // 获取架构，版本信息
            string os = SystemTools.GetOSVersion();
            string arc = ArchitectureMap[architecture];

            // 获取哈希信息
            string hashListStr = Network.HttpGet(Config.Hiper.HashMap_URL);
            GetHashMap(hashListStr);

            // 下载 Hiper 本体并验证哈希
            if (os == "Windows")
            {
                // 下载

                Downloads.AddDList(Config.Hiper.Download_URL + $"{OSMap[os]}-{arc}/hiper.exe", Config.Hiper.WorkPath + "hiper.exe", HashMap[$"{OSMap[os]}-{arc}/hiper.exe"]);
                Downloads.AddDList(Config.Hiper.Download_URL + $"{OSMap[os]}-{arc}/wintun.dll", Config.Hiper.WorkPath + "wintun.dll", HashMap[$"{OSMap[os]}-{arc}/wintun.dll"]);
                Downloads.Async(model: false).Wait();

                // 校验
                if (vaildHash)
                {
                    string hash = HashTools.GetFileSHA1(Config.Hiper.WorkPath + "hiper.exe");
                    if (hash != HashMap[$"{OSMap[os]}-{arc}/hiper.exe"])
                    {
                        throw new NotImplementedException("Hiper 主程序哈希值错误");
                    }

                    hash = HashTools.GetFileSHA1(Config.Hiper.WorkPath + "wintun.dll");
                    if (hash != HashMap[$"{OSMap[os]}-{arc}/wintun.dll"])
                    {
                        throw new NotImplementedException("WinTun 支持库哈希值错误");
                    }
                }

                return Config.Hiper.WorkPath + "/hiper.exe";
            }
            else
            {
                // 下载本体
                string remotePath = Config.Hiper.Download_URL + $"{OSMap[os]}-{arc}/hiper";
                Downloads.Plan1(remotePath, Config.Hiper.WorkPath + "hiper", $"{OSMap[os]}-{arc}/hiper");
                string hash = HashTools.GetFileSHA1(Config.Hiper.WorkPath + "hiper");
                if (vaildHash)
                {
                    if (hash != HashMap[$"{OSMap[os]}-{arc}/hiper"])
                    {
                        throw new NotImplementedException("Hiper 主程序哈希值错误");
                    }
                }

                return Config.Hiper.WorkPath + "hiper";
            }
        }

        public static void DownloadCert(string code)
        {
            string url = $"https://cert.mcer.cn/{code}.yml";
            Downloads.Plan1(url, Config.Hiper.WorkPath + $"config.yml");
        }
    }
}