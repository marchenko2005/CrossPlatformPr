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

                Console.WriteLine("Final result: " + result);
                File.WriteAllText(outputFilePath, result);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: File not found at path " + inputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            Console.ReadLine();
        }
    }
}
