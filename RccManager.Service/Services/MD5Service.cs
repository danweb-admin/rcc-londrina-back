using System;
using RccManager.Domain.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace RccManager.Service.Services
{
    public class MD5Service : IMD5Service
    {

        public bool CompareMD5(string password, string passwordMD5)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                var password_ = ReturnMD5(password);
                if (VerifyHash(md5Hash, passwordMD5, password_))
                    return true;
                else
                    return false;
            }
        }

        public string ReturnHash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public string ReturnMD5(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return ReturnHash(md5Hash, password);
            }
        }

        public bool VerifyHash(MD5 md5Hash, string input, string hash)
        {
            StringComparer compara = StringComparer.OrdinalIgnoreCase;

            if (0 == compara.Compare(input, hash))
                return true;
            else
                return false;
        }
    }
}

