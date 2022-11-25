using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BianCore.Tools
{
    public static class Encryption
    {
        public struct RSASecretKey
        {
            public RSASecretKey(string privateKey, string publicKey)
            {
                PrivateKey = privateKey;
                PublicKey = publicKey;
            }
            public string PublicKey { get; set; }
            public string PrivateKey { get; set; }
            public override string ToString()
            {
                return string.Format(
                    "PrivateKey: {0}\r\nPublicKey: {1}", PrivateKey, PublicKey);
            }
        }

        /// <summary>
        /// generate RSA secret key
        /// </summary>
        /// <param name="keySize">the size of the key,must from 384 bits to 16384 bits in increments of 8 </param>
        /// <returns></returns>
        public static RSASecretKey GenerateRSASecretKey(int keySize)
        {
            RSASecretKey rsaKey = new RSASecretKey();
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize))
            {
                rsaKey.PrivateKey = rsa.ToXmlString(true);
                rsaKey.PublicKey = rsa.ToXmlString(false);
            }
            return rsaKey;
        }
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
