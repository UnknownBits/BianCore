using System.Collections.Generic;
using System.IO;

namespace BianCore.Tools
{
    public static class FileTools
    {
        public static FileInfo[] SearchFile(string directoryPath, string pattern)
        {
            DirectoryInfo info = new DirectoryInfo(directoryPath);
            DirectoryInfo[] directoryInfos = info.GetDirectories();
            List<FileInfo> fileInfos = new List<FileInfo>();
            foreach (var directory in directoryInfos)
                fileInfos.AddRange(directory.GetFiles());
            List<FileInfo> results = new List<FileInfo>();
            foreach (FileInfo file in fileInfos)
                if (file.Name.Contains(pattern)) 
                    results.Add(file);
            return results.ToArray();
        }
    }
}
