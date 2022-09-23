using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mauve.Security;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mauve.Tests.Core.Security
{
    [TestClass]
    public class RijndaelServiceTests
    {
        [TestMethod]
        [DataRow("this is a test", false)]
        public void Encryption(string input, bool expectedResult)
        {
            try
            {
                using (var rijndael = new RijndaelCryptographyService())
                {
                    string encryptionResult = rijndael.Encrypt(input);
                    bool result = input.Equals(encryptionResult);
                    Assert.AreEqual(expectedResult, result);
                }
            } catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        [DataRow("this is a test", true)]
        public void Decryption(string input, bool expectedResult)
        {
            try
            {
                using (var rijndael = new RijndaelCryptographyService())
                {
                    string encryptionResult = rijndael.Encrypt(input);
                    string decryptionResult = rijndael.Decrypt<string>(encryptionResult);
                    bool result = input.Equals(decryptionResult);
                    Assert.AreEqual(expectedResult, result);
                }
            } catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
