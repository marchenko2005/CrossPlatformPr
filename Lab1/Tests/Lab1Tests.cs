using NUnit.Framework;
using System;
using System.Collections.Generic;
using Lab1;

namespace Lab1.Tests
{
    [TestFixture]
    public class AnagramHelperTests
    {
        [Test]
        public void CalculateCharacterCounts_WithValidInput_ReturnsCorrectCharacterCounts()
        {
            string inputWord = "hello";
            var expectedCounts = new Dictionary<char, int>
            {
                {'h', 1},
                {'e', 1},
                {'l', 2},
                {'o', 1}
            };

            var result = AnagramHelper.CalculateCharacterCounts(inputWord);

            Assert.AreEqual(expectedCounts, result);
        }

        [Test]
        public void CalculateCharacterCounts_WithEmptyString_ThrowsArgumentException()
        {
            string inputWord = "";

            var ex = Assert.Throws<ArgumentException>(() => AnagramHelper.CalculateCharacterCounts(inputWord));
            Assert.That(ex.Message, Is.EqualTo("¬х≥дне слово не може бути порожн≥м або null."));
        }

        [Test]
        public void CalculateFactorial_WithValidInput_ReturnsCorrectFactorial()
        {
            int number = 5;
            long expectedFactorial = 120;

            var result = AnagramHelper.CalculateFactorial(number);

            Assert.AreEqual(expectedFactorial, result);
        }

        [Test]
        public void CalculateFactorial_WithNegativeNumber_ThrowsArgumentException()
        {
            int number = -1;

            var ex = Assert.Throws<ArgumentException>(() => AnagramHelper.CalculateFactorial(number));
            Assert.That(ex.Message, Is.EqualTo("„исло не може бути негативним."));
        }

        [Test]
        public void CalculateUniquePermutations_WithUniqueCharacters_ReturnsCorrectPermutations()
        {
            string inputWord = "abcd";
            long expectedPermutations = 24;

            var result = AnagramHelper.CalculateUniquePermutations(inputWord);

            Assert.AreEqual(expectedPermutations, result);
        }

        [Test]
        public void CalculateUniquePermutations_WithDuplicateCharacters_ReturnsCorrectPermutations()
        {
            string inputWord = "aabb";
            long expectedPermutations = 6;

            var result = AnagramHelper.CalculateUniquePermutations(inputWord);

            Assert.AreEqual(expectedPermutations, result);
        }

        [Test]
        public void CalculateUniquePermutations_WithEmptyString_ThrowsArgumentException()
        {
            string inputWord = "";

            var ex = Assert.Throws<ArgumentException>(() => AnagramHelper.CalculateUniquePermutations(inputWord));
            Assert.That(ex.Message, Is.EqualTo("¬х≥дне слово не може бути порожн≥м або null."));
        }
    }
}
