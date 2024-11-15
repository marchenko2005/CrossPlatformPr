using System;
using System.IO;
using System.Text;

namespace Lab1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Шляхи до вхідного та вихідного файлів
            string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string inputFilePath = Path.Combine(projectRoot, "INPUT.TXT");
            string outputFilePath = Path.Combine(projectRoot, "OUTPUT.TXT");

            try
            {
                // Перевірка наявності вхідного файлу
                if (!File.Exists(inputFilePath))
                    throw new FileNotFoundException($"Вхідний файл не знайдено: {inputFilePath}");

                // Зчитування вхідного слова з файлу
                string inputWord = File.ReadAllText(inputFilePath)?.Trim() ?? string.Empty;

                // Обчислення кількості унікальних перестановок
                long uniquePermutations = AnagramHelper.CalculateUniquePermutations(inputWord);

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
            Console.ReadLine();
        }
    }
}
