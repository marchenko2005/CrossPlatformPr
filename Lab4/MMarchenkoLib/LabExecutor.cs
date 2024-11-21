using System;
using System.IO;


namespace MMarchenkoLib
{
    public class LabExecutor
    {
        public void ExecuteLab(string lab)
        {
            // Визначення шляхів для файлів
            string inputFilePath = GetInputFilePath(lab);
            string outputFilePath = GetOutputFilePath();

            // Перевірка та створення файлів
            EnsureFilesExist(lab, inputFilePath, outputFilePath);

            // Виконання відповідної лабораторної
            switch (lab.ToLower())
            {
                case "lab1":
                    Lab1.Process(inputFilePath, outputFilePath);
                    break;
                case "lab2":
                    Lab2.Process(inputFilePath, outputFilePath);
                    break;
                case "lab3":
                    Lab3.Process(inputFilePath, outputFilePath);
                    break;
                default:
                    Console.WriteLine($"Лабораторна \"{lab}\" не знайдена.");
                    break;
            }
        }

        private string GetInputFilePath(string lab)
        {
            // Шлях до вхідного файлу для обраної лабораторної
            string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            return Path.Combine(projectRoot, $"{lab.ToUpper()}_INPUT.TXT");
        }

        private string GetOutputFilePath()
        {
            // Шлях до вихідного файлу OUTPUT.TXT
            string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            return Path.Combine(projectRoot, "OUTPUT.TXT");
        }

        private void EnsureFilesExist(string lab, string inputFilePath, string outputFilePath)
        {
            // Перевірка та створення вхідного файлу
            if (!File.Exists(inputFilePath))
            {
                string defaultContent = lab switch
                {
                    "lab1" => "solo",
                    "lab2" => "3 6\r\n000\r\n101\r\n100\r\n",
                    "lab3" => "3\r\n0 1 0\r\n1 0 1\r\n0 1 0",
                    _ => "Приклад вхідних даних..."
                };
                File.WriteAllText(inputFilePath, defaultContent);
                Console.WriteLine($"Файл створено: {inputFilePath}");
            }

            // Перевірка та створення вихідного файлу
            if (!File.Exists(outputFilePath))
            {
                File.WriteAllText(outputFilePath, "");
                Console.WriteLine($"Файл створено: {outputFilePath}");
            }
        }
    }
}