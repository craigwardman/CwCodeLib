using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CwCodeLib.Tests.Serialization
{
    [TestClass]
    public class BinaryFormatterTests
    {
        [TestMethod]
        public void TestBinarySerialize()
        {
            // arrange
            var subject = new TestDataContract() { Hello = "world", Goodbye = "universe" };

            // act
            var bytes = CwCodeLib.Serialization.BinaryFormatter<TestDataContract>.Serialize(subject);

            // assert
            Assert.IsNotNull(bytes);
            Assert.IsTrue(bytes.Length > 0);
        }

        [TestMethod]
        public void TestBinaryDeserialize()
        {
            // arrange
            var original = new TestDataContract() { Hello = "world", Goodbye = "universe" };
            var bytes = CwCodeLib.Serialization.BinaryFormatter<TestDataContract>.Serialize(original);

            // act
            var deserialized = CwCodeLib.Serialization.BinaryFormatter<TestDataContract>.Deserialize(bytes);

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
            var bytes = CwCodeLib.Serialization.BinaryFormatter<int>.Serialize(subject);

            // assert
            Assert.IsNotNull(bytes);
            Assert.IsTrue(bytes.Length > 0);
        }

        [TestMethod]
        public void TestSerializeNullValue()
        {
            // arrange
            TestDataContract subject = null;

            // act
            var bytes = CwCodeLib.Serialization.BinaryFormatter<TestDataContract>.Serialize(subject);

            // assert
            Assert.IsNull(bytes);
        }

        [TestMethod]
        public void TestDeserializeValueType()
        {
            // arrange
            var subject = 1;

            // act
            var bytes = CwCodeLib.Serialization.BinaryFormatter<int>.Serialize(subject);
            var deserialized = CwCodeLib.Serialization.BinaryFormatter<int>.Deserialize(bytes);

            // assert
            Assert.AreEqual(subject, deserialized);
        }

        [TestMethod]
        public void TestDeserializeNullValue()
        {
            // arrange
            TestDataContract subject = null;

            // act
            var bytes = CwCodeLib.Serialization.BinaryFormatter<TestDataContract>.Serialize(subject);
            var deserialized = CwCodeLib.Serialization.BinaryFormatter<TestDataContract>.Deserialize(bytes);

            // assert
            Assert.IsNull(deserialized);
        }

        [Serializable]
        public class TestDataContract
        {
            public string Hello { get; set; }
            public string Goodbye { get; set; }
        }
    }
}
