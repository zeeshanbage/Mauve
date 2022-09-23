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
        [DataRow(32, false)]
        [DataRow(32L, false)]
        [DataRow(3.14, false)]
        [DataRow(3.14f, false)]
        [DataRow(false, false)]
        public void Encryption(object input, bool expectedResult)
        {
            try
            {
                using (var rijndael = new RijndaelCryptographyService())
                {
                    object encryptionResult = rijndael.Encrypt(input);
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
        [DataRow(32, true)]
        [DataRow(32L, true)]
        [DataRow(3.14, true)]
        [DataRow(3.14f, true)]
        [DataRow(false, true)]
        public void Decryption(object input, bool expectedResult)
        {
            try
            {
                using (var rijndael = new RijndaelCryptographyService())
                {
                    string encryptionResult = rijndael.Encrypt(input);
                    object decryptionResult = rijndael.Decrypt<object>(encryptionResult);
                    bool result = input.Equals(decryptionResult);
                    Assert.AreEqual(expectedResult, result);
                }
            } catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        [DataRow("this is a test", true)]
        [DataRow(32, true)]
        [DataRow(32L, true)]
        [DataRow(3.14, true)]
        [DataRow(3.14f, true)]
        [DataRow(false, true)]
        public void DelayedDecryption(object input, bool expectedResult)
        {
            try
            {
                byte[] iv = null;
                byte[] key = null;
                string encryptedValue = string.Empty;
                using (var rijndael = new RijndaelCryptographyService())
                {
                    iv = rijndael.InitializationVector;
                    key = rijndael.Key;
                    encryptedValue = rijndael.Encrypt(input);
                    Assert.AreNotEqual(input, encryptedValue);
                }

                using (var rijndael = new RijndaelCryptographyService(key, iv))
                {
                    object decryptionResult = rijndael.Decrypt<object>(encryptedValue);
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
