using System;
using System.CommandLine;
using System.CommandLine.Invocation;

class Program
{
    static async Task Main(string[] args)
    {
        var rootCommand = new RootCommand("Lab Console Application");

        var versionCommand = new Command("version", "Вывод информации о версии приложения");
        versionCommand.Handler = CommandHandler.Create(() =>
        {
            Console.WriteLine("Автор: MMarchenko");
            Console.WriteLine("Версия: 1.0.0");
        });

        var runCommand = new Command("run", "Запустить лабораторную работу")
        {
            new Option<string>("--lab", "Номер лабораторной (lab1, lab2, lab3)"),
            new Option<string>("--input", () => "INPUT.TXT", "Путь к входному файлу"),
            new Option<string>("--output", () => "OUTPUT.TXT", "Путь к выходному файлу")
        };

        runCommand.Handler = CommandHandler.Create<string, string, string>((lab, input, output) =>
        {
            if (string.IsNullOrEmpty(lab))
            {
                Console.WriteLine("Необходимо указать номер лабораторной (lab1, lab2 или lab3).");
                return;
            }

            try
            {
                var executor = new LabExecutor();
                executor.ExecuteLab(lab, input, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка выполнения: {ex.Message}");
            }
        });

        var setPathCommand = new Command("set-path", "Установить путь к папке с файлами")
        {
            new Option<string>("--path", "Путь к папке")
        };

        setPathCommand.Handler = CommandHandler.Create<string>((path) =>
        {
            if (!string.IsNullOrEmpty(path))
            {
                Environment.SetEnvironmentVariable("LAB_PATH", path);
                Console.WriteLine($"Путь установлен: {path}");
            }
            else
            {
                Console.WriteLine("Укажите корректный путь.");
            }
        });

        rootCommand.AddCommand(versionCommand);
        rootCommand.AddCommand(runCommand);
        rootCommand.AddCommand(setPathCommand);

        await rootCommand.InvokeAsync(args);
    }
}
