using System;
using System.IO;

namespace MMarchenkoLib
{
    public class Lab3
    {
        public static void Process(string inputFilePath, string outputFilePath)
        {
            try
            {
                if (!File.Exists(inputFilePath))
                    throw new FileNotFoundException($"Вхідний файл не знайдено: {inputFilePath}");

                string[] lines = File.ReadAllLines(inputFilePath);
                GraphChecker checker = new GraphChecker(lines);
                bool result = checker.IsTree();

                File.WriteAllText(outputFilePath, result ? "YES" : "NO");
                Console.WriteLine($"Граф є деревом: {(result ? "YES" : "NO")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }

    public class GraphChecker
    {
        private int[,] adjacencyMatrix;
        private int verticesCount;

        public GraphChecker(string[] lines)
        {
            LoadGraph(lines);
        }

        private void LoadGraph(string[] lines)
        {
            verticesCount = int.Parse(lines[0]);
            adjacencyMatrix = new int[verticesCount, verticesCount];

            for (int i = 0; i < verticesCount; i++)
            {
                var row = lines[i + 1].Split();
                for (int j = 0; j < verticesCount; j++)
                {
                    adjacencyMatrix[i, j] = int.Parse(row[j]);
                }
            }
        }

        public bool IsTree()
        {
            return !HasCycle() && IsConnected() && HasCorrectEdgeCount();
        }

        private bool HasCycle()
        {
            var visited = new bool[verticesCount];
            return DetectCycle(-1, 0, visited);
        }

        private bool DetectCycle(int parent, int vertex, bool[] visited)
        {
            visited[vertex] = true;
            for (int i = 0; i < verticesCount; i++)
            {
                if (adjacencyMatrix[vertex, i] == 1)
                {
                    if (!visited[i])
                    {
                        if (DetectCycle(vertex, i, visited))
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

        private bool IsConnected()
        {
            var visited = new bool[verticesCount];
            DepthFirstSearch(0, visited);
            return Array.TrueForAll(visited, v => v);
        }

        private void DepthFirstSearch(int vertex, bool[] visited)
        {
            visited[vertex] = true;
            for (int i = 0; i < verticesCount; i++)
            {
                if (adjacencyMatrix[vertex, i] == 1 && !visited[i])
                {
                    DepthFirstSearch(i, visited);
                }
            }
        }

        private bool HasCorrectEdgeCount()
        {
            int edgeCount = 0;
            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = i + 1; j < verticesCount; j++)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        edgeCount++;
                    }
                }
            }
            return edgeCount == verticesCount - 1;
        }
    }
}
