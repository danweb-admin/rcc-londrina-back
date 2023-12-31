using System;
using System.Security.Cryptography;
using System.Text;

namespace RccManager.Service.Helper;

public static class Utils
{
    public static DateTime formatDate(string date)
    {

        date = date.Replace("/", "");
        var year = int.Parse(date.Substring(4));
        var day = int.Parse(date.Substring(0, 2));
        var month = int.Parse(date.Substring(2, 2));

        return new DateTime(year, month, day);
    }

    public static DateTime formaTime(string time)
    {
        time = time.Replace(":", "");
        var hours = int.Parse(time.Substring(0, 2));
        var minutes = int.Parse(time.Substring(2));

        var now = DateTime.Now;

        return new DateTime(now.Year, now.Month, now.Day, hours, minutes, 0);
    }

    public static string Encrypt(string text)
    {
        string key = Environment.GetEnvironmentVariable("CryptKey");
        string iv = Environment.GetEnvironmentVariable("CryptIV");

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }

    }

    public static string Decrypt(string text)
    {
        string key = Environment.GetEnvironmentVariable("CryptKey");
        string iv = Environment.GetEnvironmentVariable("CryptIV");

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(text)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}

