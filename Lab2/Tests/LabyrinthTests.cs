using NUnit.Framework;
using System;
using Lab2;

namespace Tests
{
    [TestFixture]
    public class LabyrinthProcessorTests
    {
        [Test]
        public void ValidateInputNotEmpty_WithEmptyInput_ThrowsArgumentException()
        {
            string input = "";
            var ex = Assert.Throws<ArgumentException>(() => LabyrinthProcessor.ValidateInputNotEmpty(input));
            Assert.AreEqual("Вхідний файл порожній або містить лише пробіли.", ex.Message);
        }

        [Test]
        public void ValidateInputNotEmpty_WithWhitespaceInput_ThrowsArgumentException()
        {
            string input = "    ";
            var ex = Assert.Throws<ArgumentException>(() => LabyrinthProcessor.ValidateInputNotEmpty(input));
            Assert.AreEqual("Вхідний файл порожній або містить лише пробіли.", ex.Message);
        }

        [Test]
        public void ValidateGridSize_WithNonPositiveSize_ThrowsArgumentException()
        {
            int gridSize = 0;
            var ex = Assert.Throws<ArgumentException>(() => LabyrinthProcessor.ValidateGridSize(gridSize));
            Assert.AreEqual("Розмір лабіринту повинен бути додатнім.", ex.Message);
        }

        [Test]
        public void ValidateStepCount_WithNegativeSteps_ThrowsArgumentException()
        {
            int steps = -1;
            var ex = Assert.Throws<ArgumentException>(() => LabyrinthProcessor.ValidateStepCount(steps));
            Assert.AreEqual("Кількість кроків не може бути негативною.", ex.Message);
        }

        [Test]
        public void ValidateGrid_WithInvalidCharacter_ThrowsArgumentException()
        {
            char[,] grid = {
                { '0', '0', '2' },
                { '0', '1', '0' },
                { '0', '0', '0' }
            };
            var ex = Assert.Throws<ArgumentException>(() => LabyrinthProcessor.ValidateGrid(grid, 3));
            Assert.AreEqual("Неправильний формат вхідних даних: символ у рядку 1, колонка 3 повинен бути '0' або '1'.", ex.Message);
        }

        [Test]
        public void ParseInput_WithValidInput_ReturnsCorrectValues()
        {
            string input = "3 2\n000\n010\n000";
            var (gridSize, steps, grid) = LabyrinthProcessor.ParseInput(input);

            Assert.AreEqual(3, gridSize);
            Assert.AreEqual(2, steps);
            Assert.AreEqual('0', grid[0, 0]);
            Assert.AreEqual('1', grid[1, 1]);
        }

        [Test]
        public void ParseInput_WithInvalidFormat_ThrowsArgumentException()
        {
            string input = "3 a\n000\n010\n000";
            var ex = Assert.Throws<ArgumentException>(() => LabyrinthProcessor.ParseInput(input));
            Assert.AreEqual("Неправильний формат вхідних даних: розмір лабіринту та кількість кроків повинні бути цілими числами.", ex.Message);
        }

        [Test]
        public void CalculatePaths_WithExample1_ReturnsCorrectPathCount()
        {
            int gridSize = 3;
            int steps = 6;
            char[,] grid = {
                { '0', '0', '0' },
                { '1', '0', '1' },
                { '1', '0', '0' }
            };

            int result = LabyrinthProcessor.CalculatePaths(gridSize, steps, grid);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void CalculatePaths_WithExample2_ReturnsCorrectPathCount()
        {
            int gridSize = 2;
            int steps = 8;
            char[,] grid = {
                { '0', '1' },
                { '1', '0' }
            };

            int result = LabyrinthProcessor.CalculatePaths(gridSize, steps, grid);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void CalculatePaths_WithExample3_ReturnsCorrectPathCount()
        {
            int gridSize = 3;
            int steps = 6;
            char[,] grid = {
                { '0', '0', '0' },
                { '1', '1', '1' },
                { '0', '0', '0' }
            };

            int result = LabyrinthProcessor.CalculatePaths(gridSize, steps, grid);
            Assert.AreEqual(0, result);
        }

    }
}
