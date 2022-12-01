namespace BianCore.Modules.Minecraft
{
    public class Launcher
    {
        public string VersionsPath { get; set; }

        /// <summary>
        /// Launcher 类的构造方法
        /// </summary>
        /// <param name="versionsPath">versions 文件夹的路径 E.g. D:\Minecraft\.minecraft\versions</param>
        public Launcher(string versionsPath)
        {
            VersionsPath = versionsPath;
        }


    }
}
