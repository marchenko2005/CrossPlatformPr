using System;
using System.IO;
using System.Text;

namespace Lab2
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

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Вхідний файл не знайдено.");
                return;
            }

            string labyrinthData = File.ReadAllText(inputFilePath);

            try
            {
                // Перевірка, чи вхідні дані не порожні
                LabyrinthProcessor.ValidateInputNotEmpty(labyrinthData);

                // Розбір та перевірка вхідних даних
                var (gridSize, steps, grid) = LabyrinthProcessor.ParseInput(labyrinthData);

                // Перевірка розміру лабіринту
                LabyrinthProcessor.ValidateGridSize(gridSize);

                // Перевірка кількості кроків
                LabyrinthProcessor.ValidateStepCount(steps);

                // Перевірка коректності лабіринту
                LabyrinthProcessor.ValidateGrid(grid, gridSize);

                // Обчислення кількості шляхів
                int result = LabyrinthProcessor.CalculatePaths(gridSize, steps, grid);

                // Запис результату у вихідний файл
                File.WriteAllText(outputFilePath, result.ToString());
                Console.WriteLine("Лабіринт оброблено успішно.");
                Console.WriteLine($"Результат записано у файл: {outputFilePath}");
                Console.WriteLine($"Кількість унікальних шляхів: {result}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка вхідних даних: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Невідома помилка: {ex.Message}");
            }
            Console.ReadLine();
        }
    }
}
