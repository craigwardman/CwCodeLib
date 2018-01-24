using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CwCodeLib.Tests.Serialization
{
    [TestClass]
    public class XmlFormatterTests
    {
        [TestMethod]
        public void TestXmlSerialize()
        {
            // arrange
            var subject = new TestDataContract() { Hello = "world", Goodbye = "universe" };

            // act
            var xml = CwCodeLib.Serialization.XmlFormatter<TestDataContract>.Serialize(subject);

            // assert
            Assert.IsFalse(string.IsNullOrEmpty(xml));
            Assert.IsTrue(xml.Contains("Hello"));
            Assert.IsTrue(xml.Contains("world"));
            Assert.IsTrue(xml.Contains("Goodbye"));
            Assert.IsTrue(xml.Contains("universe"));
        }

        [TestMethod]
        public void TestXmlDeserialize()
        {
            // arrange
            var original = new TestDataContract() { Hello = "world", Goodbye = "universe" };
            var xml = CwCodeLib.Serialization.XmlFormatter<TestDataContract>.Serialize(original);

            // act
            var deserialized = CwCodeLib.Serialization.XmlFormatter<TestDataContract>.Deserialize(xml);

            // assert
            Assert.AreEqual(original.Hello, deserialized.Hello);
            Assert.AreEqual(original.Goodbye, deserialized.Goodbye);
        }

        [TestMethod]
        public void TestSerializeValueType()
        {
            // arrange
            var subject = 1;

            // act
            var xml = CwCodeLib.Serialization.XmlFormatter<int>.Serialize(subject);

            // assert
            Assert.IsFalse(string.IsNullOrEmpty(xml));
        }

        [TestMethod]
        public void TestSerializeNullValue()
        {
            // arrange
            TestDataContract subject = null;

            // act
            var xml = CwCodeLib.Serialization.XmlFormatter<TestDataContract>.Serialize(subject);

            // assert
            Assert.IsFalse(string.IsNullOrEmpty(xml));
        }

        public class TestDataContract
        {
            public string Hello { get; set; }
            public string Goodbye { get; set; }
        }
    }
}
