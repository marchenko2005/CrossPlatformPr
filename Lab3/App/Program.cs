using System;
using System.IO;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string inputFilePath = Path.Combine(projectRoot, "INPUT.TXT");
            string outputFilePath = Path.Combine(projectRoot, "OUTPUT.TXT");

            try
            {
                GraphChecker graphChecker = new GraphChecker(inputFilePath);
                string result = graphChecker.IsTree() ? "YES" : "NO";

                Console.WriteLine("Результат перевірки: " + result);
                File.WriteAllText(outputFilePath, result);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Помилка: файл не знайдено за шляхом " + inputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Сталася помилка: " + ex.Message);
            }
            Console.ReadLine(); 
        }
    }
}
