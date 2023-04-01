using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.Tools
{
    public class MultiDownloadCore
    {
        public bool IsDownloading { get; private set; }
        public List<Task> DownloadPool { get; private set; } = new List<Task>();

        public MultiDownloadCore() { }

        public DownloadProgress StartDownload(IEnumerable<DownloadInfo> downInfos, int threadCount = 64)
        {
            DownloadProgress progress = new DownloadProgress { TotalCount = downInfos.ToArray().Length };
            Task.Run(() =>
            {
                int nowThreadCount = 0;
                if (IsDownloading) throw new NotImplementedException("当前类已有下载任务执行。");
                IsDownloading = true;
                try
                {
                    foreach (DownloadInfo info in downInfos)
                    {
                        Task task = Task.Run(async () =>
                        {
                            try
                            {
                                while (nowThreadCount < threadCount) Task.Delay(10).Wait();

                                nowThreadCount++;
                                HttpClient client = new HttpClient();
                                var response = await client.GetAsync(info.Url);
                                using var stream = await response.Content.ReadAsStreamAsync();
                                using FileStream fs = File.Create(info.FileName);
                                byte[] buffer = new byte[1024 * 1024 * 8];
                                int readLength = stream.Read(buffer, 0, buffer.Length);
                                while (readLength > 0)
                                {
                                    readLength = stream.Read(buffer, 0, buffer.Length);
                                    fs.Write(buffer, 0, readLength);
                                    fs.Flush();

                                    buffer = new byte[1024 * 1024 * 8];
                                }

                                nowThreadCount--;
                                lock (progress)
                                {
                                    progress.CompleteCount++;
                                    progress.SuccessCount++;
                                }
                            }
                            catch
                            {
                                lock (progress)
                                {
                                    progress.CompleteCount++;
                                    progress.FailedCount++;
                                }
                            }
                            finally
                            {
                                progress.ProgressPercent = Math.Round(progress.CompleteCount / (double)progress.TotalCount, 2);
                            }
                        });
                        DownloadPool.Add(task);
                    }
                }
                catch { }
                finally
                {
                    IsDownloading = false;
                }
            });

            // 检测是否完成下载
            Task.Run(() =>
            {
                while (progress.CompleteCount < progress.TotalCount) Task.Delay(10).Wait();
                progress.IsCompleted = true;
            });
            return progress;
        }
    }

    public class DownloadInfo
    {
        public string Url { get; set; }
        public string FileName { get; set; }
    }

    public class DownloadProgress
    {
        public double ProgressPercent { get; set; }
        public int TotalCount { get; set; }
        public int CompleteCount { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
