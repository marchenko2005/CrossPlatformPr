using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ClassLib
{
    public static class Lab1
    {
        public static void Execute(string inputFilePath, string outputFilePath)
        {
            try
            {
                // Читання слова з файлу
                var word = ReadWord(inputFilePath);

                // Обчислення кількості унікальних перестановок
                var calculations = new Calculations();
                long permutationsCount = calculations.CalculatePermutations(word);

                // Запис результату в файл
                WriteResult(outputFilePath, permutationsCount);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка при виконанні Lab1: {e.Message}");
            }
        }

        private static string ReadWord(string inputFilePath)
        {
            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException("Вхідний файл не знайдено.");

            var lines = File.ReadAllLines(inputFilePath)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrEmpty(line))
                .ToList();

            if (lines.Count != 1)
                throw new InvalidOperationException("Вхідний файл повинен містити одне слово.");

            return lines[0];
        }

        private static void WriteResult(string outputFilePath, long result)
        {
            File.WriteAllText(outputFilePath, result.ToString(CultureInfo.InvariantCulture));
        }
    }

    public class Calculations
    {
        public long CalculatePermutations(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || !input.All(char.IsLetter))
                throw new ArgumentException("Вхідне слово повинно містити лише літери.");

            var letterCounts = input.GroupBy(c => c).Select(group => group.Count());
            long totalPermutations = Factorial(input.Length);

            foreach (var count in letterCounts)
                totalPermutations /= Factorial(count);

            return totalPermutations;
        }

        private long Factorial(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
                result *= i;
            return result;
        }
    }
}
