using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.Tools
{
    public class Log : IDisposable
    {
        private FileStream fileStream;
        private bool disposedValue;

        public enum Level { INFO, WARN, ERROR }

        public Log(string logPath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(logPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));
            fileStream = new FileStream(logPath, FileMode.Create);
        }

        /// <summary>
        /// 输出日志。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="moduleName">模块名称（例如 "Net"）。</param>
        /// <param name="content">日志内容。</param>
        public async Task WriteLine(Level level, string moduleName, string content)
        {
            await Task.Run(() =>
            {
                lock (fileStream)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes($"[{DateTime.Now.ToString("HH:mm:ss")}] [{level}] [{moduleName}] {content}\n");
                    fileStream.Write(buffer, 0, buffer.Length);
                    fileStream.Flush();
                }
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) 
                    fileStream.Close();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
