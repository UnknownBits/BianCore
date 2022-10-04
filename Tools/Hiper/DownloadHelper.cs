using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BianCore.Tools.Hiper
{
    public static class DownloadHelper
    {
        public const string HIPER_DOWNLOAD_URL = "https://gitcode.net/to/hiper/-/raw/master/";
        public const string HIPER_PACKAGES_URL = HIPER_DOWNLOAD_URL + "packages.sha1";
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
                HashMap.Add(keyValue[1], keyValue[0]);
            }
        }

        /// <summary>
        /// 下载 Hiper。
        /// </summary>
        /// <param name="architecture">系统架构。</param>
        /// <returns></returns>
        public static void DownloadHiper(Architecture architecture)
        {
            // 获取架构，版本信息
            string os = SystemTools.GetOSVersion();
            string arc = ArchitectureMap[architecture];

            // 获取哈希信息
            string hashListStr = Network.HttpGet(HIPER_PACKAGES_URL);
            GetHashMap(hashListStr);

            // 下载 Hiper 本体并验证哈希
            if (os == "Windows")
            {
                // 下载本体
                string remotePath = HIPER_DOWNLOAD_URL + $"{os}-{arc}/hiper.exe";
                Downloads.Plan1(remotePath, "%AppData%\\BianCore\\hiper.exe");
                string hash = HashTools.GetFileSHA1("%AppData%\\BianCore\\hiper.exe");
                if (hash != HashMap[remotePath])
                {
                    throw new NotImplementedException("The file hash value is incorrect.");
                }

                // 下载 WinTun
                remotePath = HIPER_DOWNLOAD_URL + $"{os}-{arc}/wintun.dll";
                Downloads.Plan1(remotePath, "%AppData%\\BianCore\\wintun.dll");
                hash = HashTools.GetFileSHA1("%AppData%\\BianCore\\wintun.dll");
                if (hash != HashMap[remotePath])
                {
                    throw new NotImplementedException("The file hash value is incorrect.");
                }
            }
            else
            {
                // 下载本体
                string remotePath = HIPER_DOWNLOAD_URL + $"{os}-{arc}/hiper";
                Downloads.Plan1(remotePath, "%AppData%\\BianCore\\hiper");
                string hash = HashTools.GetFileSHA1("%AppData%\\BianCore\\hiper");
                if (hash != HashMap[remotePath])
                {
                    throw new NotImplementedException("The file hash value is incorrect.");
                }
            }
        }
    }
}
