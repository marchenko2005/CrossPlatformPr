using NUnit.Framework;

namespace Labyrinth.Tests
{
    public class LabyrinthTests
    {
        [Test]
        public void Test_Labyrinth_Example1()
        {
            bool[,] blocked = {
                { false, false, false },
                { true, false, true },
                { true, false, false }
            };

            Labyrinth labyrinth = new Labyrinth(3, 6, blocked);
            long result = labyrinth.CalculatePaths();

            Assert.AreEqual(5, result);
        }

        [Test]
        public void Test_Labyrinth_Example2()
        {
            bool[,] blocked = {
                { false, true },
                { true, false }
            };

            Labyrinth labyrinth = new Labyrinth(2, 8, blocked);
            long result = labyrinth.CalculatePaths();

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Test_Labyrinth_Example3()
        {
            bool[,] blocked = {
                { false, false, false },
                { true, true, true },
                { false, false, false }
            };

            Labyrinth labyrinth = new Labyrinth(3, 6, blocked);
            long result = labyrinth.CalculatePaths();

            Assert.AreEqual(0, result);
        }
    }
}
