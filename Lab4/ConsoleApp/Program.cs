using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using ClassLib;

namespace ConsoleApp
{
    class Program
    {
        private const string LabPathVariable = "LAB_PATH";
        private const string DefaultInputFileName = "INPUT.TXT";
        private const string DefaultOutputFileName = "OUTPUT.TXT";

        static int Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "ConsoleApp",
                Description = "Консольний застосунок для запуску лабораторних робіт"
            };

            app.HelpOption("-?|-h|--help");

            // Команда "version"
            app.Command("version", command =>
            {
                command.Description = "Вивести інформацію про програму";
                command.OnExecute(() =>
                {
                    Console.WriteLine("Автор:");
                    Console.WriteLine("Версія: 1.0.0");
                });
            });

            // Команда "set-path"
            app.Command("set-path", command =>
            {
                command.Description = "Встановити шлях до папки з вхідними та вихідними файлами";
                var pathOption = command.Option("-p|--path <PATH>", "Шлях до папки", CommandOptionType.SingleValue).IsRequired();

                command.OnExecute(() =>
                {
                    string path = pathOption.Value();
                    Environment.SetEnvironmentVariable(LabPathVariable, path, EnvironmentVariableTarget.User);
                    Console.WriteLine($"LAB_PATH встановлено на {path}");
                });
            });

            // Команда "run"
            app.Command("run", command =>
            {
                command.Description = "Запустити одну з лабораторних робіт";
                var labArgument = command.Argument("lab", "Лабораторна робота для запуску (lab1, lab2 або lab3)").IsRequired();
                var inputOption = command.Option("-i|--input <INPUT>", "Вхідний файл", CommandOptionType.SingleValue);
                var outputOption = command.Option("-o|--output <OUTPUT>", "Вихідний файл", CommandOptionType.SingleValue);

                command.OnExecute(() =>
                {
                    string labName = labArgument.Value;
                    string inputFilePath = GetFilePath(inputOption, DefaultInputFileName);
                    string outputFilePath = GetFilePath(outputOption, DefaultOutputFileName);

                    switch (labName.ToLower())
                    {
                        case "lab1":
                            Lab1.Execute(inputFilePath, outputFilePath);
                            break;
                        case "lab2":
                            Lab2.Execute(inputFilePath, outputFilePath);
                            break;
                        case "lab3":
                            Lab3.Execute(inputFilePath, outputFilePath);
                            break;
                        default:
                            Console.WriteLine("Неправильно вказана лабораторна робота. Використовуйте 'lab1', 'lab2' або 'lab3'.");
                            break;
                    }
                });
            });

            return app.Execute(args);
        }

        private static string GetFilePath(CommandOption option, string defaultFileName)
        {
            if (option.HasValue())
            {
                return option.Value();
            }

            string labPath = Environment.GetEnvironmentVariable(LabPathVariable);

            if (!string.IsNullOrEmpty(labPath))
            {
                return Path.Combine(labPath, defaultFileName);
            }

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), defaultFileName);
        }
    }
}
