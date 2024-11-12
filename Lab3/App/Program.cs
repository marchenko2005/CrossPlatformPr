using System;
using System.IO;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string inputFilePath = Path.Combine(projectRoot, "INPUT.TXT");
            string outputFilePath = Path.Combine(projectRoot, "OUTPUT.TXT");


            GraphChecker graphChecker = new GraphChecker(inputFilePath);

            string result = graphChecker.IsTree() ? "YES" : "NO";

            File.WriteAllText(outputFilePath, result);
        }
    }
}
