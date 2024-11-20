using System;
using System.IO;
using System.Text;
using Lab1;
using Lab2;
using Lab3;

namespace MMarchenkoLib
{
    public class LabExecutor
    {
        // Метод для виконання конкретної лабораторної роботи
        public void ExecuteLab(string lab, string inputPath, string outputPath)
        {
            switch (lab.ToLower())
            {
                case "lab1":
                    RunLab1(inputPath, outputPath);
                    break;
                case "lab2":
                    RunLab2(inputPath, outputPath);
                    break;
                case "lab3":
                    RunLab3(inputPath, outputPath);
                    break;
                default:
                    throw new ArgumentException($"Невідома лабораторна робота: {lab}");
            }
        }

        // Реалізація Lab1
        private void RunLab1(string inputPath, string outputPath)
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"Вхідний файл не знайдено: {inputPath}");

            string inputWord = File.ReadAllText(inputPath)?.Trim() ?? string.Empty;

            long uniquePermutations = AnagramHelper.CalculateUniquePermutations(inputWord);

            File.WriteAllText(outputPath, uniquePermutations.ToString());
            Console.WriteLine($"Lab1 завершено. Кількість унікальних перестановок: {uniquePermutations}");
        }

        // Реалізація Lab2
        private void RunLab2(string inputPath, string outputPath)
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"Вхідний файл не знайдено: {inputPath}");

            string labyrinthData = File.ReadAllText(inputPath);

            LabyrinthProcessor.ValidateInputNotEmpty(labyrinthData);

            var (gridSize, steps, grid) = LabyrinthProcessor.ParseInput(labyrinthData);
            LabyrinthProcessor.ValidateGridSize(gridSize);
            LabyrinthProcessor.ValidateStepCount(steps);
            LabyrinthProcessor.ValidateGrid(grid, gridSize);

            int result = LabyrinthProcessor.CalculatePaths(gridSize, steps, grid);

            File.WriteAllText(outputPath, result.ToString());
            Console.WriteLine($"Lab2 завершено. Кількість унікальних шляхів: {result}");
        }

        // Реалізація Lab3
        private void RunLab3(string inputPath, string outputPath)
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"Вхідний файл не знайдено: {inputPath}");

            GraphChecker graphChecker = new GraphChecker(inputPath);
            string result = graphChecker.IsTree() ? "YES" : "NO";

            File.WriteAllText(outputPath, result);
            Console.WriteLine($"Lab3 завершено. Граф є деревом: {result}");
        }
    }
}
