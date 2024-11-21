using System;
using System.CommandLine;
using System.CommandLine.Invocation;

class Program
{
    static async Task Main(string[] args)
    {
        // Головна команда
        var rootCommand = new RootCommand("Консольний додаток для лабораторних робіт");

        // Команда "version"
        var versionCommand = new Command("version", "Вивести інформацію про версію програми");
        versionCommand.Handler = CommandHandler.Create(() =>
        {
            Console.WriteLine("Автор: Марія Марченко");
            Console.WriteLine("Версія: 1.0.0");
        });

        // Команда "run"
        var runCommand = new Command("run", "Запустити лабораторну роботу")
        {
            new Option<string>("--lab", "Номер лабораторної роботи (lab1, lab2, lab3)")
            {
                IsRequired = true // Параметр --lab є обов'язковим
            }
        };

        runCommand.Handler = CommandHandler.Create<string>((lab) =>
        {
            if (string.IsNullOrEmpty(lab))
            {
                Console.WriteLine("Необхідно вказати номер лабораторної роботи (lab1, lab2 або lab3).");
                return;
            }

            try
            {
                var executor = new LabExecutor();
                executor.ExecuteLab(lab);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка виконання: {ex.Message}");
            }
        });

        // Команда "set-path"
        var setPathCommand = new Command("set-path", "Встановити шлях до папки з файлами")
        {
            new Option<string>("--path", "Шлях до папки")
            {
                IsRequired = true // Параметр --path є обов'язковим
            }
        };

        setPathCommand.Handler = CommandHandler.Create<string>((path) =>
        {
            if (!string.IsNullOrEmpty(path))
            {
                Environment.SetEnvironmentVariable("LAB_PATH", path);
                Console.WriteLine($"Шлях встановлено: {path}");
            }
            else
            {
                Console.WriteLine("Вкажіть коректний шлях.");
            }
        });

        // Додавання команд до головної команди
        rootCommand.AddCommand(versionCommand);
        rootCommand.AddCommand(runCommand);
        rootCommand.AddCommand(setPathCommand);

        // Виконання команди
        await rootCommand.InvokeAsync(args);
    }
}
