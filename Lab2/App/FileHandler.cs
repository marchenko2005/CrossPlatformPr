using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Labyrinth
{

    public static class FileHandler
    {
        private static readonly string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        private static readonly string InputFileName = Path.Combine(projectRoot, "INPUT.TXT");
        private static readonly string OutputFileName = Path.Combine(projectRoot, "OUTPUT.TXT");

        public static (int N, int K, bool[,] blocked) ReadInput()
        {
            Console.WriteLine($"Current directory: {projectRoot}");
            Console.WriteLine($"Looking for input file at: {InputFileName}");

            if (!File.Exists(InputFileName))
            {
                throw new FileNotFoundException($"Input file not found at: {InputFileName}");
            }

            var lines = File.ReadAllLines(InputFileName)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrEmpty(line))
                .ToList();

            if (lines.Count == 0)
            {
                throw new InvalidOperationException("Input file is empty.");
            }

            // Перевіряємо, що перший рядок містить два числа
            string[] parameters = lines[0].Split();
            if (parameters.Length != 2)
            {
                throw new InvalidOperationException("Input file must contain exactly two numbers in the first line.");
            }

            int N = int.Parse(parameters[0]);
            int K = int.Parse(parameters[1]);

            // Читання карти лабіринту
            bool[,] blocked = new bool[N, N];
            for (int i = 1; i <= N; i++)
            {
                string row = lines[i];
                for (int j = 0; j < N; j++)
                {
                    blocked[i - 1, j] = row[j] == '1';
                }
            }

            return (N, K, blocked);
        }

        public static void WriteResult(long result)
        {
            Console.WriteLine($"Writing result to: {OutputFileName}");

            try
            {
                File.WriteAllText(OutputFileName, result.ToString(CultureInfo.InvariantCulture));
                Console.WriteLine("Result successfully written.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write the result: {ex.Message}");
            }
        }
    }
}
