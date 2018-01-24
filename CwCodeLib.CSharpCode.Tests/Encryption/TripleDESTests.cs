using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CwCodeLib.Tests.Encryption
{
    [TestClass]
    public class TripleDESTests
    {
        [TestMethod]
        public void TestTripleDES()
        {
            // arrange
            var originalPlaintext = "hello world";
            var iv = CwCodeLib.Encryption.TripleDES.GenerateIV();
            var key = CwCodeLib.Encryption.TripleDES.GenerateKey();
            var cipher = new CwCodeLib.Encryption.TripleDES(key, iv);

            // act
            var cipherText = cipher.Encrypt(originalPlaintext);
            var decrypted = cipher.Decrypt(cipherText);

            // assert
            Assert.AreEqual(originalPlaintext, decrypted);
            Assert.AreNotEqual(originalPlaintext, cipherText);
        }
    }
}
