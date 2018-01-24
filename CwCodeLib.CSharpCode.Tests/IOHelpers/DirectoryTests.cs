using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CwCodeLib.Tests.IOHelpers
{
    [TestClass]
    public class DirectoryTests
    {
        private const string SandBoxPath = "DirectoryTestsSandbox";

        [TestInitialize]
        public void Init()
        {
            System.IO.Directory.CreateDirectory(SandBoxPath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            System.IO.Directory.Delete(SandBoxPath, true);
        }

        [TestMethod]
        public void TestEnsureDirectories()
        {
            // arrange
            string newFolders = "a\\b\\c\\";
            string path = System.IO.Path.Combine(SandBoxPath, newFolders);

            // act
            CwCodeLib.IOHelpers.Directory.EnsureDirectories(path);

            // assert
            Assert.IsTrue(System.IO.Directory.Exists(path));
        }

        [TestMethod]
        public void TestGetAvailableFilename()
        {
            // arrange
            var filename = "filename.txt";
            System.IO.File.WriteAllText(System.IO.Path.Combine(SandBoxPath, filename), "hello world");

            // act
            var nextFilename = CwCodeLib.IOHelpers.Directory.GetAvailableFilename(SandBoxPath, "filename.txt");

            // assert
            Assert.AreNotEqual(filename, nextFilename);
            Assert.AreEqual(System.IO.Path.GetExtension(filename), System.IO.Path.GetExtension(nextFilename));
        }
    }
}