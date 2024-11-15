using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    public class AnagramHelper
    {
        public static Dictionary<char, int> CalculateCharacterCounts(string inputWord)
        {
            // Перевірка вхідного слова
            ValidateInputWord(inputWord);

            return inputWord.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        }

        public static long CalculateFactorial(int number)
        {
            // Перевірка на негативне значення числа
            ValidateNonNegativeNumber(number);

            long result = 1;
            for (int i = 2; i <= number; i++)
                result *= i;
            return result;
        }

        public static long CalculateUniquePermutations(string inputWord)
        {
            // Перевірка вхідного слова
            ValidateInputWord(inputWord);

            var characterCounts = CalculateCharacterCounts(inputWord);
            long totalPermutations = CalculateFactorial(inputWord.Length);

            foreach (var count in characterCounts.Values)
                totalPermutations /= CalculateFactorial(count);

            return totalPermutations;
        }

        // Приватний метод для перевірки вхідного слова
        private static void ValidateInputWord(string inputWord)
        {
            if (string.IsNullOrWhiteSpace(inputWord))
                throw new ArgumentException("Вхідне слово не може бути порожнім або null.");
        }

        // Приватний метод для перевірки на негативне значення числа
        private static void ValidateNonNegativeNumber(int number)
        {
            if (number < 0)
                throw new ArgumentException("Число не може бути негативним.");
        }
    }
}
