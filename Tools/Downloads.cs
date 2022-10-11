using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace BianCore.Tools
{
    public static class Downloads
    {
        private static List<string> DList = new List<string>();
        private static List<string> DPath = new List<string>();
        private static List<string> DHash = new List<string>();

        public static void AddDList(string link, string path, string hash = null)
        {
            DPath.Add(path);
            DList.Add(link);
            DHash.Add(hash);
        }

        public static void ClearList()
        {
            DPath.Clear();
            DList.Clear();
            DHash.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="save">保存路径（带文件名）</param>
        /// <param name="hash">哈希校验（可选）</param>
        /// <param name="model">批量下载（可选）</param>
        public static void Plan1(string url = null, string save = null, string hash = null, bool model = true)
        {
            using (var web = new WebClient())
            {
                if (model == false)
                {
                    for (int i = 0; i < DList.Count; i++)
                    {
                        if (!File.Exists(DPath[i]) || DHash[i] != HashTools.GetFileSHA1(DPath[i]))
                        {
                            if (!Directory.Exists(Path.GetDirectoryName(DPath[i])))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(DPath[i]));
                            }
                            try
                            {
                                web.DownloadFile(DList[i], DPath[i]);
                            }
                            catch { }
                        }
                    }
                    ClearList();
                }
                else
                {
                    if (!File.Exists(save) || hash != HashTools.GetFileSHA1(save))
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(save)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(save));
                        }
                        try
                        {
                            web.DownloadFile(url, save);
                        }
                        catch { }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="save">保存路径（带文件名）</param>
        /// <param name="hash">哈希校验（可选）</param>
        /// <param name="model">批量下载（可选）</param>
        public static async Task Async(string Url = null, string save = null, string hash = null, bool model = true)
        {
            using (var web = new WebClient())
            {
                if (model == false)
                {
                    for (int i = 0; i < DList.Count; i++)
                    {
                        
                        if (!File.Exists(DPath[i]) || DHash[i] != HashTools.GetFileSHA1(DPath[i]))
                        {
                            if (!Directory.Exists(Path.GetDirectoryName(DPath[i])))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(DPath[i]));
                            }
                            try
                            {
                                await web.DownloadFileTaskAsync(DList[i], DPath[i]);
                            }
                            catch { }
                        }
                    }
                    ClearList();
                }
                else
                {
                    if (!File.Exists(save) || hash != HashTools.GetFileSHA1(save))
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(save)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(save));
                        }
                        try
                        {
                            await web.DownloadFileTaskAsync(Url, save);
                        }
                        catch { }
                    }
                }
            }
        }
    }
}
