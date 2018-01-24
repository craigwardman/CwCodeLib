using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CwCodeLib.Tests.Encryption
{
    [TestClass]
    public class MD5Tests
    {
        [TestMethod]
        public void TestMD5()
        {
            // arrange
            var input = "hello world";

            // act
            var output = CwCodeLib.Encryption.MD5.GetHashString(input);

            // assert
            Assert.AreEqual("5EB63BBBE01EEED093CB22BB8F5ACDC3", output);
        }
    }
}
