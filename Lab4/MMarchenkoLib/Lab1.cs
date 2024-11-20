using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MMarchenkoLib
{
    public class Lab1
    {
        public static void Process(string inputFilePath, string outputFilePath)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                // Перевірка наявності вхідного файлу
                if (!File.Exists(inputFilePath))
                    throw new FileNotFoundException($"Вхідний файл не знайдено: {inputFilePath}");

                // Зчитування вхідного слова з файлу
                string inputWord = File.ReadAllText(inputFilePath)?.Trim() ?? string.Empty;

                // Обчислення кількості унікальних перестановок
                long uniquePermutations = CalculateUniquePermutations(inputWord);

                // Запис результату у вихідний файл
                File.WriteAllText(outputFilePath, uniquePermutations.ToString());

                // Виведення результату
                Console.WriteLine("Обчислення анаграм");
                Console.WriteLine($"Кількість унікальних перестановок для слова \"{inputWord}\": {uniquePermutations}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        public static long CalculateUniquePermutations(string inputWord)
        {
            ValidateInputWord(inputWord);

            var characterCounts = CalculateCharacterCounts(inputWord);
            long totalPermutations = CalculateFactorial(inputWord.Length);

            foreach (var count in characterCounts.Values)
                totalPermutations /= CalculateFactorial(count);

            return totalPermutations;
        }

        private static Dictionary<char, int> CalculateCharacterCounts(string inputWord)
        {
            return inputWord.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        }

        private static long CalculateFactorial(int number)
        {
            if (number < 0)
                throw new ArgumentException("Число не може бути негативним.");

            long result = 1;
            for (int i = 2; i <= number; i++)
                result *= i;
            return result;
        }

        private static void ValidateInputWord(string inputWord)
        {
            if (string.IsNullOrWhiteSpace(inputWord))
                throw new ArgumentException("Вхідне слово не може бути порожнім або null.");
        }
    }
}
