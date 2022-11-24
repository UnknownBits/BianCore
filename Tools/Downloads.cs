using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.Tools
{
    public  class Downloads
    {
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="save">保存路径</param>
        /// <param name="model">覆盖模式</param>
        public void Plan1(string url,string save,bool model)
        {
            using (var web = new WebClient())
            {
                if (!File.Exists(save) || model)
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

        public void Plan1(Dictionary<string,string> value,bool model)
        {
            using (var web = new WebClient())
            {

            }
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
