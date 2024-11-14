using System;
using System.IO;

namespace ClassLib
{
    public static class Lab3
    {
        public static void Execute(string inputFilePath, string outputFilePath)
        {
            try
            {
                var graphChecker = new GraphChecker(inputFilePath);
                string result = graphChecker.IsTree() ? "YES" : "NO";

                File.WriteAllText(outputFilePath, result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка при виконанні Lab3: {e.Message}");
            }
        }
    }

    public class GraphChecker
    {
        private int[,] adjacencyMatrix;
        private int verticesCount;

        public GraphChecker(string inputFile)
        {
            ReadInput(inputFile);
        }

        private void ReadInput(string inputFile)
        {
            var line = File.ReadAllText(inputFile).Trim();
            var parts = line.Split(' ');

            verticesCount = int.Parse(parts[0]);
            adjacencyMatrix = new int[verticesCount, verticesCount];

            string matrixData = parts[1];
            int index = 0;
            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    adjacencyMatrix[i, j] = matrixData[index] - '0';
                    index++;
                }
            }
        }

        public bool IsTree()
        {
            bool[] visited = new bool[verticesCount];
            if (HasCycle(-1, 0, visited))
                return false;

            for (int i = 0; i < verticesCount; i++)
            {
                if (!visited[i])
                    return false;
            }

            return true;
        }

        private bool HasCycle(int parent, int vertex, bool[] visited)
        {
            visited[vertex] = true;

            for (int i = 0; i < verticesCount; i++)
            {
                if (adjacencyMatrix[vertex, i] == 1)
                {
                    if (!visited[i])
                    {
                        if (HasCycle(vertex, i, visited))
                            return true;
                    }
                    else if (i != parent)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
