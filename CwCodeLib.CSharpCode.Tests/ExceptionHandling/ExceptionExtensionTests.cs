using System;
using CwCodeLib.ExceptionHandling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CwCodeLib.Tests
{
    [TestClass]
    public class ExceptionExtensionTests
    {
        [TestMethod]
        public void TestMessageEx_ContainsAllExceptions()
        {
            // arrange
            var innerEx = new Exception("Inner exception message");
            var outerEx = new Exception("Outer exception message", innerEx);

            // act
            string message = outerEx.MessageEx();

            // assert
            Assert.IsTrue(message.Contains(innerEx.Message));
            Assert.IsTrue(message.Contains(outerEx.Message));
        }

        [TestMethod]
        public void TestMessageEx_NullDoesNotError()
        {
            // arrange
            Exception ex = null;

            // act
            string message = ex.MessageEx();

            // assert
            Assert.IsNull(ex);
        }
    }
}
