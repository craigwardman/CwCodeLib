using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CwCodeLib.Tests.Encryption
{
    [TestClass]
    public class SHA512Tests
    {
        [TestMethod]
        public void TestSHA512()
        {
            // arrange
            var input = "hello world";

            // act
            var output = CwCodeLib.Encryption.SHA512.GetHashString(input);

            // assert
            Assert.AreEqual("309ECC489C12D6EB4CC40F50C902F2B4D0ED77EE511A7C7A9BCD3CA86D4CD86F989DD35BC5FF499670DA34255B45B0CFD830E81F605DCF7DC5542E93AE9CD76F", output);
        }
    }
}
