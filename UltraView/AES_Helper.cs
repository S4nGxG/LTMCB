using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UltraView
{
    internal class AES_Helper
    {
        private static readonly string Key = "this_is_a_very_secure_key_1234567890!";
        private static readonly string IV = "1234567890abcdef";

        public static byte[] EncryptBytes(byte[] data)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key.Substring(0, 32));
                aes.IV = Encoding.UTF8.GetBytes(IV.Substring(0, 16));
                using (var ms = new MemoryStream())
                using (var cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }

        public static byte[] DecryptBytes(byte[] data)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key.Substring(0, 32));
                aes.IV = Encoding.UTF8.GetBytes(IV.Substring(0, 16));
                using (var ms = new MemoryStream())
                using (var cryptoStream = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }

        public static byte[] StringToBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string BytesToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

    }
}
