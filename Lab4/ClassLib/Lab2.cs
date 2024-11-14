using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ClassLib
{
    public static class Lab2
    {
        public static void Execute(string inputFilePath, string outputFilePath)
        {
            try
            {
                var (N, K, blocked) = FileHandler.ReadInput(inputFilePath);
                var labyrinth = new Labyrinth(N, K, blocked);
                long result = labyrinth.CalculatePaths();

                FileHandler.WriteResult(outputFilePath, result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка при виконанні Lab2: {e.Message}");
            }
        }
    }

    public static class FileHandler
    {
        public static (int N, int K, bool[,] blocked) ReadInput(string inputFilePath)
        {
            var lines = File.ReadAllLines(inputFilePath);
            var parameters = lines[0].Split();
            int N = int.Parse(parameters[0]);
            int K = int.Parse(parameters[1]);

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

        public static void WriteResult(string outputFilePath, long result)
        {
            File.WriteAllText(outputFilePath, result.ToString(CultureInfo.InvariantCulture));
        }
    }

    public class Labyrinth
    {
        private readonly int N;
        private readonly int K;
        private readonly bool[,] blocked;
        private readonly long[,,] dp;
        private static readonly int[] dx = { 1, -1, 0, 0 };
        private static readonly int[] dy = { 0, 0, 1, -1 };

        public Labyrinth(int n, int k, bool[,] blockedCells)
        {
            N = n;
            K = k;
            blocked = blockedCells;
            dp = new long[N, N, K + 1];
        }

        public long CalculatePaths()
        {
            dp[0, 0, 0] = 1;

            for (int step = 0; step < K; step++)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (dp[i, j, step] > 0 && !blocked[i, j])
                        {
                            for (int d = 0; d < 4; d++)
                            {
                                int ni = i + dx[d];
                                int nj = j + dy[d];
                                if (ni >= 0 && ni < N && nj >= 0 && nj < N && !blocked[ni, nj])
                                {
                                    dp[ni, nj, step + 1] += dp[i, j, step];
                                }
                            }
                        }
                    }
                }
            }

            return dp[N - 1, N - 1, K];
        }
    }
}
