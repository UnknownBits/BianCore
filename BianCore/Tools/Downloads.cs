using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BianCore.Tools
{
    public static class Downloads
    {
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="save">保存路径</param>
        /// <param name="model">覆盖模式</param>
        public static void Plan1(string url, string save, bool model)
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
                    catch
                    {

                    }
                }
            }
        }
        public static void Plan1(string url, string save, string hash)
        {
            using (var web = new WebClient())
            {
                if (!File.Exists(save) || hash != Encryption.GetFileSHA1(save))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(save)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(save));
                    }
                    try
                    {
                        web.DownloadFile(url, save);
                    }
                    catch
                    {

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
        public static async Task Async(string url, string save, bool model)
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
                        await web.DownloadFileTaskAsync(url, save);
                    }
                    catch
                    {

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
        public static async Task Async(string url, string save, string hash)
        {
            using (var web = new WebClient())
            {
                if (!File.Exists(save) || hash != Encryption.GetFileSHA1(save))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(save)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(save));
                    }
                    try
                    {
                        await web.DownloadFileTaskAsync(url, save);
                    }
                    catch
                    {

                    }
                }

            }
        }
    }
}
