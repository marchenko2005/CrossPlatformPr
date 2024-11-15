using NUnit.Framework;
using System;
using System.IO;

namespace Lab3.Tests
{
    [TestFixture]
    public class GraphCheckerTests
    {
        [Test]
        public void TestGraphIsTree()
        {
            // Симулюємо вхідні дані
            string input = "3\n0 1 0\n1 0 1\n0 1 0";
            string tempFile = CreateTempFile(input);

            GraphChecker graphChecker = new GraphChecker(tempFile);
            Assert.IsTrue(graphChecker.IsTree(), "Граф є деревом, але метод повернув false.");
        }

        [Test]
        public void TestGraphIsNotTree_CycleExists()
        {
            // Граф із циклом
            string input = "3\n0 1 1\n1 0 1\n1 1 0";
            string tempFile = CreateTempFile(input);

            GraphChecker graphChecker = new GraphChecker(tempFile);
            Assert.IsFalse(graphChecker.IsTree(), "Граф містить цикл, але метод повернув true.");
        }

        [Test]
        public void TestGraphIsNotTree_Disconnected()
        {
            // Роз'єднаний граф
            string input = "4\n0 1 0 0\n1 0 0 1\n0 0 0 0\n0 1 0 0";
            string tempFile = CreateTempFile(input);

            GraphChecker graphChecker = new GraphChecker(tempFile);
            Assert.IsFalse(graphChecker.IsTree(), "Граф не є зв'язним, але метод повернув true.");
        }

        [Test]
        public void TestInvalidFile_EmptyFile()
        {
            string input = "";
            string tempFile = CreateTempFile(input);

            Assert.Throws<FormatException>(() => new GraphChecker(tempFile), "Файл порожній.");
        }

        [Test]
        public void TestInvalidFile_WrongMatrixSize()
        {
            string input = "3\n0 1\n1 0 1\n0 1 0";
            string tempFile = CreateTempFile(input);

            Assert.Throws<FormatException>(() => new GraphChecker(tempFile), "Матриця суміжності має некоректний розмір.");
        }

        [Test]
        public void TestInvalidFile_InvalidCharacters()
        {
            string input = "3\n0 1 A\n1 0 1\n0 1 0";
            string tempFile = CreateTempFile(input);

            Assert.Throws<FormatException>(() => new GraphChecker(tempFile), "Матриця містить некоректні символи.");
        }

        [Test]
        public void TestGraphIsTree_LargerTree()
        {
            // Велике дерево
            string input = "4\n0 1 1 0\n1 0 0 1\n1 0 0 0\n0 1 0 0";
            string tempFile = CreateTempFile(input);

            GraphChecker graphChecker = new GraphChecker(tempFile);
            Assert.IsTrue(graphChecker.IsTree(), "Граф є деревом, але метод повернув false.");
        }

        [Test]
        public void TestGraphHasCycleAndConnected()
        {
            // Граф зв'язний, але містить цикл
            string input = "4\n0 1 1 0\n1 0 1 1\n1 1 0 1\n0 1 1 0";
            string tempFile = CreateTempFile(input);

            GraphChecker graphChecker = new GraphChecker(tempFile);
            Assert.IsFalse(graphChecker.IsTree(), "Граф містить цикл, але метод повернув true.");
        }

        // Метод для створення тимчасового файлу з вхідними даними
        private string CreateTempFile(string input)
        {
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, input);
            return tempFile;
        }
    }
}
