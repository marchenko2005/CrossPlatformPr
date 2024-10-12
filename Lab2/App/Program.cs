using System;

namespace Labyrinth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var (N, K, blocked) = FileHandler.ReadInput("INPUT.TXT");

            Labyrinth labyrinth = new Labyrinth(N, K, blocked);
            long result = labyrinth.CalculatePaths();

            FileHandler.WriteOutput("OUTPUT.TXT", result);
        }
    }
}
