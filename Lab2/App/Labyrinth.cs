using System;

namespace Labyrinth
{
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
