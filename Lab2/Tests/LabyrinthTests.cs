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

            Assert.AreEqual(5, result);  // Î÷³êóºìî 5 øëÿõ³â
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

            Assert.AreEqual(0, result);  // Øëÿõ³â íåìàº, òîìó ðåçóëüòàò 0
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
            Assert.AreEqual(0, result);  
        }
    }
}
