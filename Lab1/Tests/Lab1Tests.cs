using NUnit.Framework;
using App;
using System;

namespace Tests
{
    [TestFixture]
    public class CalculationsTests
    {
        private Calculations _calculations;

        [SetUp]
        public void Setup()
        {
            _calculations = new Calculations();
        }

        [Test]
        public void TestFactorial_CorrectInput()
        {
            // Тестируем расчет факториала
            Assert.AreEqual(120, _calculations.Factorial(5));
            Assert.AreEqual(1, _calculations.Factorial(0));
            Assert.AreEqual(1, _calculations.Factorial(1));
        }

        [Test]
        public void TestCalculatePermutations_CorrectInput()
        {
            // Тестируем расчет количества перестановок
            Assert.AreEqual(12, _calculations.CalculatePermutations("solo"));
            Assert.AreEqual(180, _calculations.CalculatePermutations("letter"));
            Assert.AreEqual(1, _calculations.CalculatePermutations("aaa"));
        }

        [Test]
        public void TestCalculatePermutations_EmptyString()
        {
            // Тестируем обработку пустой строки
            var ex = Assert.Throws<ArgumentException>(() => _calculations.CalculatePermutations(""));
            Assert.AreEqual("Input string cannot be null or empty. (Parameter 'input')", ex.Message);
        }

        [Test]
        public void TestCalculatePermutations_InvalidCharacters()
        {
            // Тестируем обработку строки с недопустимыми символами
            var ex = Assert.Throws<ArgumentException>(() => _calculations.CalculatePermutations("abc123"));
            Assert.AreEqual("Input string must contain only letters. (Parameter 'input')", ex.Message);
        }
    }
}
