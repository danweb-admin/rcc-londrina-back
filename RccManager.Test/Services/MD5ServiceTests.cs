using System;
using RccManager.Service.Services;
using System.Security.Cryptography;

namespace RccManager.Test.Services
{
    public class MD5ServiceTests
    {
        [Fact]
        public void CompareMD5_MatchingPasswords_ReturnsTrue()
        {
            // Arrange
            var md5Service = new MD5Service();
            var password = "myPassword";
            var passwordMD5 = md5Service.ReturnMD5(password);

            // Act
            var result = md5Service.CompareMD5(password, passwordMD5);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CompareMD5_NonMatchingPasswords_ReturnsFalse()
        {
            // Arrange
            var md5Service = new MD5Service();
            var password1 = "myPassword";
            var password2 = "differentPassword";
            var passwordMD5 = md5Service.ReturnMD5(password1);

            // Act
            var result = md5Service.CompareMD5(password2, passwordMD5);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ReturnHash_CreatesExpectedHash()
        {
            // Arrange
            var md5Service = new MD5Service();
            using (MD5 md5Hash = MD5.Create())
            {
                var input = "myInput";
                var expectedHash = md5Service.ReturnHash(md5Hash, input);

                // Act
                var actualHash = md5Service.ReturnHash(md5Hash, input);

                // Assert
                Assert.Equal(expectedHash, actualHash);
            }
        }

        [Fact]
        public void VerifyHash_MatchingHashes_ReturnsTrue()
        {
            // Arrange
            var md5Service = new MD5Service();
            using (MD5 md5Hash = MD5.Create())
            {
                var input = "myInput";
                var hash = md5Service.ReturnHash(md5Hash, input);
                var inputMd5 = "6b7f8b556af9fbf321dfb3b6ee0d9675";

                // Act
                var result = md5Service.VerifyHash(md5Hash, inputMd5, hash);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void VerifyHash_NonMatchingHashes_ReturnsFalse()
        {
            // Arrange
            var md5Service = new MD5Service();
            using (MD5 md5Hash = MD5.Create())
            {
                var input = "myInput";
                var hash = md5Service.ReturnHash(md5Hash, input);
                var differentInput = "differentInput";

                // Act
                var result = md5Service.VerifyHash(md5Hash, differentInput, hash);

                // Assert
                Assert.False(result);
            }
        }
    }
}

