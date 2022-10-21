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
                if (!File.Exists(Config.Bing.BackGround_File) || !File.Exists(Config.Bing.BackGround_Date) || File.ReadAllText(Config.Bing.BackGround_Date)!= DateTime.Now.ToString("d"))
                {
                    Tools.Downloads.Plan1($"{API.Bing.Urlbase()}{resolution}.jpg", Config.Bing.BackGround_File);
                    Task.Run(() =>
                    {
                        FileStream fileStream = new FileStream(Config.Bing.BackGround_Date, FileMode.Create);
                        lock (fileStream)
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes(DateTime.Now.ToString("d"));
                            fileStream.Write(buffer, 0, buffer.Length);
                            fileStream.Flush();
                        }
                    });
                }
                
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