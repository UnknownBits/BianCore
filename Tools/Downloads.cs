using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bian.Tools
{
    internal class Downloads
    {
        /// <summary>
        /// 下载文件(单线程)
        /// </summary>
        /// <param name="url">下载链接</param>
        /// <param name="save">保存地址</param>
        /// <returns></returns>
        public static void Plan1(string url, string save)
        {
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
        public static async Task Plan2()
        {
            for (int i = 0; i < DList.Count; i++)
            {
                await DList[i].DownloadFileAsync(DPath[i], DName[i]);
            }

        }
    }
}
