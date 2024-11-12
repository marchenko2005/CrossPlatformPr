using NUnit.Framework;
using System.IO;

namespace Lab3
{
    public class GraphCheckerTests
    {
        [Test]
        public void TestGraphIsTree()
        {
            string inputFile = "TestInput1.TXT";
            File.WriteAllText(inputFile, "3 010101010");
            GraphChecker graphChecker = new GraphChecker(inputFile);
            Assert.IsTrue(graphChecker.IsTree());
        }

        [Test]
        public void TestGraphIsNotTree_CycleExists()
        {
            string inputFile = "TestInput2.TXT";
            File.WriteAllText(inputFile, "3 011111110");
            GraphChecker graphChecker = new GraphChecker(inputFile);
            Assert.IsFalse(graphChecker.IsTree());
        }

        [Test]
        public void TestGraphIsNotTree_Disconnected()
        {
            string inputFile = "TestInput3.TXT";
            File.WriteAllText(inputFile, "4 0100001000100000");
            GraphChecker graphChecker = new GraphChecker(inputFile);
            Assert.IsFalse(graphChecker.IsTree());
        }

        [Test]
        public void TestGraphIsTree_LargerTree()
        {
            string inputFile = "TestInput4.TXT";
            File.WriteAllText(inputFile, "4 0101001000000000"); 
            GraphChecker graphChecker = new GraphChecker(inputFile);
            Assert.IsTrue(graphChecker.IsTree());
        }
    }
}
