using System;

namespace Labyrinth
{
    internal static class Program
    {
        private static void Main()
        {
            (int N, int K, bool[,] blocked) labyrinthData;

            try
            {
                // Зчитуємо вхідні дані з файлу INPUT.TXT
                labyrinthData = FileHandler.ReadInput();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File not found: {e.FileName}");
                Console.WriteLine($"Message: {e.Message}");
                Console.WriteLine($"Stack Trace: {e.StackTrace}");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while reading input: {e.Message}");
                return;
            }

            long result;
            try
            {
                // Створюємо екземпляр класу Labyrinth і обчислюємо кількість шляхів
                var labyrinth = new Labyrinth(labyrinthData.N, labyrinthData.K, labyrinthData.blocked);
                result = labyrinth.CalculatePaths();
                Console.WriteLine($"Count of paths: {result}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during calculations: {e.Message}");
                return;
            }

            try
            {
                // Записуємо результат у файл OUTPUT.TXT
                FileHandler.WriteResult(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while writing output: {e.Message}");
                Console.WriteLine($"Stack Trace: {e.StackTrace}");
            }

            // Зупиняємо програму, чекаючи натискання клавіші Enter
            Console.ReadLine();
        }
    }
}
