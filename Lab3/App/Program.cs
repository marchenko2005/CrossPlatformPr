using System;
using System.Text;
using ClassLib;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            string inputFilePath = "INPUT.TXT";
            string outputFilePath = "OUTPUT.TXT";

            try
            {
                GraphChecker graphChecker = new GraphChecker(inputFilePath);
                string result = graphChecker.IsTree() ? "YES" : "NO";
                Console.WriteLine("Результат перевірки: " + result);
                System.IO.File.WriteAllText(outputFilePath, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Сталася помилка: " + ex.Message);
            }
            Console.ReadLine();
        }
    }
}
