using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Program
{
    static void Main(string[] arguments)
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Визначення кореневої директорії проекту
        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string inputFilePath = Path.Combine(projectRoot, "INPUT.TXT");
        string outputFilePath = Path.Combine(projectRoot, "OUTPUT.TXT");

        // Зчитування вхідного тексту
        string inputText = File.ReadAllText(inputFilePath).Trim();

        // Обчислення частот символів
        Dictionary<char, int> characterFrequency = inputText.GroupBy(character => character)
                                                             .ToDictionary(group => group.Key, group => group.Count());

        // Функція для обчислення факторіалу
        long ComputeFactorial(int number)
        {
            long factorial = 1;
            for (int i = 2; i <= number; i++)
                factorial *= i;
            return factorial;
        }

        // Обчислення кількості унікальних перестановок
        long uniqueArrangements = ComputeFactorial(inputText.Length);
        foreach (var frequency in characterFrequency.Values)
            uniqueArrangements /= ComputeFactorial(frequency);

        // Запис результатів у вихідний файл
        File.WriteAllText(outputFilePath, uniqueArrangements.ToString());
        Console.WriteLine("Task Execution Complete");
        Console.WriteLine($"Number of unique permutations for \"{inputText}\": {uniqueArrangements}");
        Console.ReadLine();
    }
}
