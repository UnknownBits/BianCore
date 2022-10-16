using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static BianCore.Tools.Log;

namespace BianCore.Tools
{
    public static class BackGround
    {
        public static class Bing
        {
            public static void Get(Resolution resolution)
            {
                Tools.Downloads.Plan1($"{API.Bing.Urlbase()}{resolution}",Config.Bing.BackGround_File);
                Task.Run(() =>
                {
                    FileStream fileStream = new FileStream(Config.Bing.BackGround_Hash, FileMode.OpenOrCreate);
                    lock (fileStream)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(Tools.HashTools.GetFileSHA1(Config.Bing.BackGround_File));
                        fileStream.Write(buffer, 0, buffer.Length);
                        fileStream.Flush();
                    }
                });
            }
            public enum Resolution
            {
                _UHD,
                _1920x1080,
                _1600x900,
                _1280x1024
            }
        }
    }
}