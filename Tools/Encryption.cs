using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BianCore.Tools
{
    public static class Encryption
    {
        public struct RSAKey
        {
            public RSAKey(string privateKey, string publicKey)
            {
                PrivateKey = privateKey;
                PublicKey = publicKey;
            }
            public string PublicKey { get; set; }
            public string PrivateKey { get; set; }
            public override string ToString()
            {
                return $"PrivateKey: {PrivateKey}{Environment.CommandLine}PublicKey: {PublicKey}";
            }
        }

        /// <summary>
        /// RSA 加密
        /// </summary>
        /// <param name="content">加密内容</param>
        /// <param name="xmlRSAKey">RSA 密钥</param>
        /// <returns></returns>
        public static byte[] RSAEncrypt(byte[] content, string xmlRSAKey)
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlRSAKey);
            byte[] encryptedData = rsa.Encrypt(content, false);
            return encryptedData;
        }

        /// <summary>
        /// RSA 解密
        /// </summary>
        /// <param name="content">解密内容</param>
        /// <param name="xmlRSAKey">RSA 密钥</param>
        /// <returns></returns>
        public static byte[] RSADecrypt(byte[] content, string xmlRSAKey)
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlRSAKey);
            byte[] decryptedData = rsa.Decrypt(content, false);
            return decryptedData;
        }

        /// <summary>
        /// 生成 RSA 密钥（XML）
        /// </summary>
        /// <returns></returns>
        public static RSAKey GenerateRSAKey()
        {
            RSAKey rsaKey = new RSAKey();
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
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

        /// <summary>
        /// 对文件进行 SHA256 哈希运算。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <returns>全小写哈希值。</returns>
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
