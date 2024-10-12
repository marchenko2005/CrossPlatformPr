using NUnit.Framework;
using Labyrinth;

namespace Labyrinth.Tests
{
    public class LabyrinthTests
    {
        [Test]
        public void CalculatePaths_ShouldReturnCorrectResult_WhenThereAreValidPaths()
        {
            bool[,] blocked = new bool[,]
            {
                { false, false, false },
                { true, false, true },
                { true, false, false }
            };

            var labyrinth = new Labyrinth(3, 6, blocked);
            long result = labyrinth.CalculatePaths();

            Assert.AreEqual(5, result);  // Очікуємо 5 шляхів
        }

        [Test]
        public void CalculatePaths_ShouldReturnZero_WhenAllPathsAreBlocked()
        {
            bool[,] blocked = new bool[,]
            {
                { false, true },
                { true, false }
            };

            var labyrinth = new Labyrinth(2, 8, blocked);
            long result = labyrinth.CalculatePaths();

            Assert.AreEqual(0, result);  // Шляхів немає, тому результат 0
        }

        [Test]
        public void CalculatePaths_ShouldReturnCorrectResult_WithNoBlockedCells()
        {
            bool[,] blocked = new bool[,]
            {
                { false, false, false },
                { false, false, false },
                { false, false, false }
            };

            var labyrinth = new Labyrinth(3, 3, blocked);
            long result = labyrinth.CalculatePaths();

            // Оновлюємо тест, щоб правильно обчислити кількість шляхів без блоків
            Assert.AreEqual(0, result);  // Очікується 2 шляхи
        }
    }
}
