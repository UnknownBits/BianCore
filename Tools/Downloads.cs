using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace BianCore.Tools
{
    public static class Downloads
    {
        /// <summary>
        /// 下载文件(单线程)
        /// </summary>
        /// <param name="url">下载链接</param>
        /// <param name="save">保存地址</param>
        /// <returns></returns>
        public static void Plan1(string url, string save)
        {
            if (!Directory.Exists(Path.GetDirectoryName(save)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(save));
            }
            using (var web = new WebClient())
            {
                web.DownloadFile(url, save);
            }
        }
        private static List<string> DList = new List<string>();
        private static List<string> DPath = new List<string>();
        private static List<string> DName = new List<string>();
        public static void AddDList(string link, string path, string name = null)
        {
            DPath.Add(path);
            DList.Add(link);
            DName.Add(name);
        }
        public static void ClearList()
        {
            DPath.Clear();
            DName.Clear();
            DList.Clear();
        }
        public static async Task Plan2(string Url = null,string save = null,string name = null ,bool model = true)
        {
            if (model == false)
            {
                for (int i = 0; i < DList.Count; i++)
                {
                    await DList[i].DownloadFileAsync(DPath[i], DName[i]);
                }
                ClearList();
            }
            await Url.DownloadFileAsync(save, name);
        }
    }
}
