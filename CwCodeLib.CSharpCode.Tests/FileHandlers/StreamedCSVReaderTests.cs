using System.Collections.Generic;
using CwCodeLib.FileHandlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CwCodeLib.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DeploymentItem("FileHandlers\\SampleFile.csv")]
        public void TestReadingCsvFile()
        {
            // arrange
            var csvFileReader = new StreamedCSVReader("SampleFile.csv");

            // act
            var firstRow = csvFileReader.ReadLine();

            // assert
            Assert.IsNotNull(firstRow);
            Assert.IsTrue(firstRow.Length > 0);
            Assert.IsFalse(string.IsNullOrEmpty(string.Join(string.Empty, firstRow)));
        }

        [TestMethod]
        [DeploymentItem("FileHandlers\\SampleFile.csv")]
        public void TestReadingCsvFileMultipleRows()
        {
            // arrange
            var csvFileReader = new StreamedCSVReader("SampleFile.csv");

            // act
            List<string[]> rows = new List<string[]>();
            rows.Add(csvFileReader.ReadLine());
            rows.Add(csvFileReader.ReadLine());
            rows.Add(csvFileReader.ReadLine());

            // assert
            foreach (var r in rows)
            {
                Assert.IsNotNull(r);
                Assert.IsTrue(r.Length > 0);
                Assert.IsFalse(string.IsNullOrEmpty(string.Join(string.Empty, r)));
            }
        }

        [TestMethod]
        [DeploymentItem("FileHandlers\\SampleFile.csv")]
        public void TestReadingCsvFileEofReturnsNull()
        {
            // arrange
            var csvFileReader = new StreamedCSVReader("SampleFile.csv");

            // act
            csvFileReader.ReadLine();
            csvFileReader.ReadLine();
            csvFileReader.ReadLine();

            var result = csvFileReader.ReadLine();

            // assert
            Assert.IsNull(result);
        }
    }
}