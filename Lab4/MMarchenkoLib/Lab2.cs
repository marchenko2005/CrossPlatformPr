using System;
using System.IO;
using System.Text;

namespace MMarchenkoLib
{
    public class Lab2
    {
        public static void Process(string inputFilePath, string outputFilePath)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                if (!File.Exists(inputFilePath))
                {
                    Console.WriteLine("Вхідний файл не знайдено.");
                    return;
                }

                string labyrinthData = File.ReadAllText(inputFilePath);

                ValidateInputNotEmpty(labyrinthData);

                var (gridSize, steps, grid) = ParseInput(labyrinthData);

                ValidateGridSize(gridSize);
                ValidateStepCount(steps);
                ValidateGrid(grid, gridSize);

                int result = CalculatePaths(gridSize, steps, grid);

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
        }

        private static void ValidateInputNotEmpty(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Вхідний файл порожній або містить лише пробіли.");
        }

        private static (int gridSize, int steps, char[,] grid) ParseInput(string input)
        {
            string[] lines = input.Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length < 2)
                throw new ArgumentException("Неправильний формат вхідних даних.");

            string[] firstLine = lines[0].Split();
            if (!int.TryParse(firstLine[0], out int gridSize) || !int.TryParse(firstLine[1], out int steps))
                throw new ArgumentException("Розмір лабіринту та кроки мають бути цілими числами.");

            char[,] grid = new char[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                string line = lines[i + 1];
                if (line.Length != gridSize)
                    throw new ArgumentException($"Рядок {i + 2} має неправильну довжину.");
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = line[j];
                }
            }

            return (gridSize, steps, grid);
        }

        private static void ValidateGridSize(int gridSize)
        {
            if (gridSize <= 0)
                throw new ArgumentException("Розмір лабіринту має бути додатнім.");
        }

        private static void ValidateStepCount(int steps)
        {
            if (steps < 0)
                throw new ArgumentException("Кількість кроків не може бути негативною.");
        }

        private static void ValidateGrid(char[,] grid, int gridSize)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[i, j] != '0' && grid[i, j] != '1')
                        throw new ArgumentException($"Недопустимий символ у лабіринті: {grid[i, j]}");
                }
            }
        }

        private static int CalculatePaths(int gridSize, int steps, char[,] grid)
        {
            int[,,] paths = new int[2, gridSize + 2, gridSize + 2];
            paths[0, 1, 1] = 1;

            for (int step = 1; step <= steps; step++)
            {
                for (int i = 1; i <= gridSize; i++)
                {
                    for (int j = 1; j <= gridSize; j++)
                    {
                        if (grid[i - 1, j - 1] == '0')
                        {
                            paths[step % 2, i, j] = paths[(step - 1) % 2, i - 1, j]
                                                  + paths[(step - 1) % 2, i + 1, j]
                                                  + paths[(step - 1) % 2, i, j - 1]
                                                  + paths[(step - 1) % 2, i, j + 1];
                        }
                    }
                }
            }

            return paths[steps % 2, gridSize, gridSize];
        }
    }
}
