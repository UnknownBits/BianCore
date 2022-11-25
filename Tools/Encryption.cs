using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BianCore.Tools
{
    public static class HashTools
    {

        /// <summary>
        /// 对文件进行 SHA1 哈希运算。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <returns>全小写哈希值。</returns>
        public static string GetFileSHA1(string filePath)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                using (FileStream file = File.OpenRead(filePath))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var data in sha1.ComputeHash(file))
                    {
                        stringBuilder.Append(data.ToString("x2"));
                    }
                    return stringBuilder.ToString();
                }
            }
        }
        public static string GetFileSHA256(string filePath)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                using (FileStream file = File.OpenRead(filePath))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var data in sha256.ComputeHash(file))
                    {
                        stringBuilder.Append(data.ToString("x2"));
                    }
                    return stringBuilder.ToString();
                }
            }
        }
    }
}
