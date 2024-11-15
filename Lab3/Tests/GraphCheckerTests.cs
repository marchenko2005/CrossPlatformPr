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
            // ��������� ����� ���
            string input = "3\n0 1 0\n1 0 1\n0 1 0";
            string tempFile = CreateTempFile(input);

            GraphChecker graphChecker = new GraphChecker(tempFile);
            Assert.IsTrue(graphChecker.IsTree(), "���� � �������, ��� ����� �������� false.");
        }

        [Test]
        public void TestGraphIsNotTree_CycleExists()
        {
            // ���� �� ������
            string input = "3\n0 1 1\n1 0 1\n1 1 0";
            string tempFile = CreateTempFile(input);

            GraphChecker graphChecker = new GraphChecker(tempFile);
            Assert.IsFalse(graphChecker.IsTree(), "���� ������ ����, ��� ����� �������� true.");
        }

        [Test]
        public void TestGraphIsNotTree_Disconnected()
        {
            // ���'������� ����
            string input = "4\n0 1 0 0\n1 0 0 1\n0 0 0 0\n0 1 0 0";
            string tempFile = CreateTempFile(input);

            GraphChecker graphChecker = new GraphChecker(tempFile);
            Assert.IsFalse(graphChecker.IsTree(), "���� �� � ��'�����, ��� ����� �������� true.");
        }

        [Test]
        public void TestInvalidFile_EmptyFile()
        {
            string input = "";
            string tempFile = CreateTempFile(input);

            Assert.Throws<FormatException>(() => new GraphChecker(tempFile), "���� �������.");
        }

        [Test]
        public void TestInvalidFile_WrongMatrixSize()
        {
            string input = "3\n0 1\n1 0 1\n0 1 0";
            string tempFile = CreateTempFile(input);

            Assert.Throws<FormatException>(() => new GraphChecker(tempFile), "������� �������� �� ����������� �����.");
        }

        [Test]
        public void TestInvalidFile_InvalidCharacters()
        {
            string input = "3\n0 1 A\n1 0 1\n0 1 0";
            string tempFile = CreateTempFile(input);

            Assert.Throws<FormatException>(() => new GraphChecker(tempFile), "������� ������ ��������� �������.");
        }

        [Test]
        public void TestGraphIsTree_LargerTree()
        {
            // ������ ������
            string input = "4\n0 1 1 0\n1 0 0 1\n1 0 0 0\n0 1 0 0";
            string tempFile = CreateTempFile(input);

            GraphChecker graphChecker = new GraphChecker(tempFile);
            Assert.IsTrue(graphChecker.IsTree(), "���� � �������, ��� ����� �������� false.");
        }

        [Test]
        public void TestGraphHasCycleAndConnected()
        {
            // ���� ��'�����, ��� ������ ����
            string input = "4\n0 1 1 0\n1 0 1 1\n1 1 0 1\n0 1 1 0";
            string tempFile = CreateTempFile(input);

            GraphChecker graphChecker = new GraphChecker(tempFile);
            Assert.IsFalse(graphChecker.IsTree(), "���� ������ ����, ��� ����� �������� true.");
        }

        // ����� ��� ��������� ����������� ����� � �������� ������
        private string CreateTempFile(string input)
        {
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, input);
            return tempFile;
        }
    }
}
