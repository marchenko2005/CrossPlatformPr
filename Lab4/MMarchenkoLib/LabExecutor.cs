using System;
using System.Text;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;

namespace MMarchenkoLib
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Головна команда
            var rootCommand = new RootCommand("Lab Console Application");

            // Команда "version"
            var versionCommand = new Command("version", "Вивести інформацію про версію програми");
            versionCommand.SetHandler(() =>
            {
                Console.WriteLine("Автор: MMarchenko");
                Console.WriteLine("Версія: 1.0.0");
            });

            // Команда "run"
            var labOption = new Option<string>("--lab", "Номер лабораторної роботи (lab1, lab2, lab3)") { IsRequired = true };
            var inputOption = new Option<string>("--input", "Шлях до вхідного файлу") { IsRequired = true };
            var outputOption = new Option<string>("--output", "Шлях до вихідного файлу") { IsRequired = true };

            var runCommand = new Command("run", "Запустити лабораторну роботу")
            {
                labOption,
                inputOption,
                outputOption
            };

            runCommand.SetHandler((string lab, string input, string output) =>
            {
                if (string.IsNullOrWhiteSpace(lab))
                {
                    Console.WriteLine("Необхідно вказати номер лабораторної роботи (lab1, lab2, lab3).");
                    return;
                }

                try
                {
                    switch (lab.ToLower())
                    {
                        case "lab1":
                            Lab1.Process(input, output);
                            break;

                        case "lab2":
                            Lab2.Process(input, output);
                            break;

                        case "lab3":
                            Lab3.Process(input, output);
                            break;

                        default:
                            Console.WriteLine($"Лабораторна робота \"{lab}\" не знайдена.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Сталася помилка під час виконання: {ex.Message}");
                }
            }, labOption, inputOption, outputOption);

            // Команда "set-path"
            var pathOption = new Option<string>("--path", "Шлях до папки") { IsRequired = true };

            var setPathCommand = new Command("set-path", "Встановити шлях до директорії з файлами")
            {
                pathOption
            };

            setPathCommand.SetHandler((string path) =>
            {
                if (!string.IsNullOrEmpty(path))
                {
                    Environment.SetEnvironmentVariable("LAB_PATH", path);
                    Console.WriteLine($"Шлях встановлено: {path}");
                }
                else
                {
                    Console.WriteLine("Необхідно вказати коректний шлях.");
                }
            }, pathOption);

            // Додавання команд до головної команди
            rootCommand.AddCommand(versionCommand);
            rootCommand.AddCommand(runCommand);
            rootCommand.AddCommand(setPathCommand);

            // Виконання команди
            await rootCommand.InvokeAsync(args);
        }
    }
}
