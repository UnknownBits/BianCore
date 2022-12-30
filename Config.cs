using BianCore.Tools;
using System;
using System.Runtime.InteropServices;

namespace BianCore
{
    public static class Config
    {
        public static char ClassPath_Separator
        {
            get
            {
                if (SystemTools.GetOSPlatform() == SystemTools.OSPlatform.Windows)
                {
                    return ';';
                }
                else
                {
                    return ':';
                }
            }
        }

        /// <summary>
        /// 项目名称（动态，可更改）
        /// </summary>
        public static string Project_Name { get; set; } = "Bian_Core";
        public static Version Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        /// <summary>
        /// 获取 App 数据根路径。（带 '\' 或 '/' 后缀）
        /// </summary>
        /// <returns>App 数据根路径。（带 '\' 或 '/' 后缀）</returns>
        public static string RootPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return @"/Applications/";
            }
            else
            {
                return @"/usr/";
            }
        }

        /// <summary>
        /// 获取该应用程序工作目录。
        /// </summary>
        /// <returns>工作目录。</returns>
        public static string Work_Path { get; } = $"{RootPath()}{Project_Name}/";
        public static string Config_Path { get; } = $"{Work_Path}Config.bian";
        public static string BackGround_Path { get; } = $"{Work_Path}Backgroud/";
        public static string BackGround_File { get; set; } = Bing.BackGround_File; // 默认Bing壁纸
        public static string Music_Path { get; } = $"{Work_Path}Music/";

        public static string Log_File { get; } = $"{Work_Path}Info/{DateTime.Now.ToString("HH-mm")}.Log";
        public static Log Log = new Log(Log_File);

        public static class Bing
        {
            public static string BackGround_File { get; } = $"{BackGround_Path}Bing.jpg";
            public static string BackGround_Date { get; } = $"{BackGround_Path}Bing.date";
        }
    }
}
