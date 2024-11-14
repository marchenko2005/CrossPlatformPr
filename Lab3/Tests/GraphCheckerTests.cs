using NUnit.Framework;
using System.IO;

namespace Lab3
{
    public class GraphCheckerTests
    {
        private readonly string testDirectory = Path.Combine(Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.Parent.Parent.FullName, "App");

        [SetUp]
        public void Setup()
        {
            if (!Directory.Exists(testDirectory))
            {
                Directory.CreateDirectory(testDirectory);
            }
        }

        [Test]
        public void TestGraphIsTree()
        {
            string inputFile = Path.Combine(testDirectory, "TestInput1.TXT");
            File.WriteAllText(inputFile, "3\n0 1 0\n1 0 1\n0 1 0");
            GraphChecker graphChecker = new GraphChecker(inputFile);
            Assert.IsTrue(graphChecker.IsTree());
        }

        [Test]
        public void TestGraphIsNotTree_CycleExists()
        {
            string inputFile = Path.Combine(testDirectory, "TestInput2.TXT");
            File.WriteAllText(inputFile, "3\n0 1 1\n1 0 1\n1 1 0");
            GraphChecker graphChecker = new GraphChecker(inputFile);
            Assert.IsFalse(graphChecker.IsTree());
        }

        [Test]
        public void TestGraphIsNotTree_Disconnected()
        {
            string inputFile = Path.Combine(testDirectory, "TestInput3.TXT");
            File.WriteAllText(inputFile, "4\n0 1 0 0\n1 0 0 1\n0 0 0 0\n0 1 0 0");
            GraphChecker graphChecker = new GraphChecker(inputFile);
            Assert.IsFalse(graphChecker.IsTree());
        }

        [Test]
        public void TestGraphIsTree_LargerTree()
        {
            string inputFile = Path.Combine(testDirectory, "TestInput4.TXT");
            File.WriteAllText(inputFile, "4\n0 1 1 0\n1 0 0 1\n1 0 0 0\n0 1 0 0");
            GraphChecker graphChecker = new GraphChecker(inputFile);
            Assert.IsTrue(graphChecker.IsTree());
        }
    }
}
