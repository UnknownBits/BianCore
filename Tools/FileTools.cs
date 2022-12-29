using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BianCore.Tools
{
    public static class FileTools
    {
        public static FileInfo[] SearchFile(string directoryPath, string pattern)
        {
            DirectoryInfo info = new DirectoryInfo(directoryPath);
            FileInfo[] fileInfos = info.EnumerateFiles(pattern, SearchOption.AllDirectories).ToArray();
            List<FileInfo> results = new List<FileInfo>();
            foreach (FileInfo file in fileInfos)
            {
                if (file.Name.Contains(pattern)) results.Add(file);
            }

            return results.ToArray();
        }
    }
}
