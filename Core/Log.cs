using BianCore.Tools;
using Newtonsoft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.Core
{
    public class Log : IDisposable
    {
        private FileStream fileStream;
        private bool disposedValue;

        public Log(string logPath)
        {
            fileStream = new FileStream(logPath, FileMode.CreateNew);
        }

        /// <summary>
        /// 写入日志。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="moduleName">模块名称（例如 "Net"）。</param>
        /// <param name="content">日志内容。</param>
        public void WriteLine(Level level, string moduleName, string content)
        {
            Task.Run(() =>
            {
                lock (fileStream)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes($"[{SystemTools.GetTimestamp("HH:MM:SS")}] [{level}] [{moduleName}] {content}\n");
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            });
        }

        public enum Level
        {
            INFO,
            WARN,
            ERROR
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    fileStream.Close();
                }

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
