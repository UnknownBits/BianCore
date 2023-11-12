using BianCore.DataType.Minecraft.Launcher;
using BianCore.Modules.Minecraft;
using BianCore.Tools;
using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("请输入 .minecraft 路径：");
            string mcPath = Console.ReadLine();

            Launcher launcher = new Launcher(mcPath);
            var vers = launcher.ScanVersions();
            for (int i = 0; i < vers.Length; i++)
            {
                Console.WriteLine($@"{i + 1}:
ID: {vers[i].ID}
AssetsIndex: {vers[i].AssetsIndexName}");
            }

            Console.Write("请输入版本序号：");
            int idx = int.Parse(Console.ReadLine()) - 1;
            var ver = vers[idx];
            Console.WriteLine($@"版本信息：
ID: {ver.ID}
AssetsIndex: {ver.AssetsIndexName}");

            var javas = Java.FindJava();
            for (int i = 0; i < javas.Length; i++)
            {
                Console.WriteLine($@"{i + 1}: 
Java 版本: {javas[i].JavaVersion}
Java 路径: {javas[i].JavaPath}");
            }
            Console.Write("请输入 Java 序号：");
            var java = javas[int.Parse(Console.ReadLine()) - 1];

            Console.Write("请输入用户名：");
            string username = Console.ReadLine();
            LaunchProperties prop = new LaunchProperties();
            prop.LaunchVersion = ver;
            prop.JVMProperties.MaxHeapSize = 4096;
            prop.GameProperties.Username = username;
            prop.GameProperties.UUID = MinecraftTools.GetOfflinePlayerUUID(username);
            prop.GameProperties.AccessToken = prop.GameProperties.UUID;
            prop.GameProperties.UserType = LaunchProperties.GamePropertiesStruct.UserTypeEnum.Legacy;
            string script = launcher.BuildLaunchScript(prop);
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = java.JavaPath,
                Arguments = script,
                CreateNoWindow = true,
                UseShellExecute = true
            };
            Process.Start(startInfo);
        }
    }
}