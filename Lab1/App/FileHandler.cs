using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace App
{
    public static class FileHandler
    {

        private static readonly string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string InputFileName = Path.Combine(projectRoot, "INPUT.TXT");
        private static readonly string OutputFileName = Path.Combine(projectRoot, "OUTPUT.TXT");

        public static string ReadWord()
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

            if (lines.Count != 1)
            {
                throw new InvalidOperationException("Input file contains more than one line.");
            }

            return lines[0];
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
