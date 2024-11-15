﻿using System;

namespace Labyrinth
{
    internal static class Program
    {
        private static void Main()
        {
            (int N, int K, bool[,] blocked) labyrinthData;

            try
            {
                labyrinthData = FileHandler.ReadInput();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File not found: {e.FileName}");
                Console.WriteLine($"Message: {e.Message}");
                Console.WriteLine($"Stack Trace: {e.StackTrace}");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while reading input: {e.Message}");
                return;
            }

            long result;
            try
            {
                var labyrinth = new Labyrinth(labyrinthData.N, labyrinthData.K, labyrinthData.blocked);
                result = labyrinth.CalculatePaths();
                Console.WriteLine($"Count of paths: {result}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during calculations: {e.Message}");
                return;
            }

            try
            {
                FileHandler.WriteResult(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while writing output: {e.Message}");
                Console.WriteLine($"Stack Trace: {e.StackTrace}");
            }

            Console.ReadLine();
        }
    }
}
