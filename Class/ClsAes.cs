using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyBanHang.Class
{
    public class ClsAes
    {
       public static string Encrypt(string plainText, string password)
        {
            using (Aes aes = Aes.Create())
            {
                var key = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes("SaltValue"));
                aes.Key = key.GetBytes(32);
                aes.IV = key.GetBytes(16);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (var writer = new StreamWriter(cryptoStream))
                        {
                            writer.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

      public  static string Decrypt(string cipherText, string password)
        {
            using (Aes aes = Aes.Create())
            {
                var key = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes("SaltValue"));
                aes.Key = key.GetBytes(32);
                aes.IV = key.GetBytes(16);

                using (var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cryptoStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
