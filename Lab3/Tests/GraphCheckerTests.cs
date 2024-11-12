using NUnit.Framework;

namespace Lab3
{
    public class GraphCheckerTests
    {
        [Test]
        public void TestGraphIsTree()
        {
            // Створюємо тимчасовий файл для тестування
            string inputFile = "TestInput.TXT";
            File.WriteAllText(inputFile, "3\n0 1 0\n1 0 1\n0 1 0");
            GraphChecker graphChecker = new GraphChecker(inputFile);
            Assert.IsTrue(graphChecker.IsTree());
        }

        [Test]
        public void TestGraphIsNotTree()
        {
            // Створюємо тимчасовий файл для тестування
            string inputFile = "TestInput2.TXT";
            File.WriteAllText(inputFile, "3\n0 1 1\n1 0 1\n1 1 0");
            GraphChecker graphChecker = new GraphChecker(inputFile);
            Assert.IsFalse(graphChecker.IsTree());
        }
    }
}
