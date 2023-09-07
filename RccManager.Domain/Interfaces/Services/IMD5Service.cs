using System.Security.Cryptography;

namespace RccManager.Domain.Interfaces.Services;


public interface IMD5Service
{
    string ReturnMD5(string password);
    bool CompareMD5(string password, string passwordMD5);
    string ReturnHash(MD5 md5Hash, string input);
    bool VerifyHash(MD5 md5Hash, string input, string hash);

}

