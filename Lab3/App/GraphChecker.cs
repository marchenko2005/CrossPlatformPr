using System;
using System.IO;

namespace Lab3
{
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
            Console.WriteLine("Reading input data...");

            string[] lines = File.ReadAllLines(inputFile);
            if (lines.Length == 0)
                throw new FormatException("The input file is empty.");

            if (!int.TryParse(lines[0].Trim(), out verticesCount))
                throw new FormatException("The first line should be an integer representing the number of vertices.");

            Console.WriteLine("Number of vertices: " + verticesCount);

            adjacencyMatrix = new int[verticesCount, verticesCount];

            for (int i = 0; i < verticesCount; i++)
            {
                string[] row = lines[i + 1].Trim().Split(' ');
                if (row.Length != verticesCount)
                    throw new FormatException("The adjacency matrix row length does not match the expected size.");

                for (int j = 0; j < verticesCount; j++)
                {
                    adjacencyMatrix[i, j] = int.Parse(row[j]);
                }
            }

            Console.WriteLine("Adjacency matrix:");
            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    Console.Write(adjacencyMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public bool IsTree()
        {
            bool[] visited = new bool[verticesCount];
            int edgeCount = 0;

            Console.WriteLine("Checking for cycles...");

            if (HasCycle(-1, 0, visited))
            {
                Console.WriteLine("Cycle detected in the graph.");
                return false;
            }

            Console.WriteLine("Checking for connectivity...");

            int visitedCount = 0;
            foreach (bool v in visited)
            {
                if (v) visitedCount++;
            }

            Console.WriteLine("Number of visited vertices: " + visitedCount);

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

            Console.WriteLine("Edge count: " + edgeCount);
            Console.WriteLine("Expected edges for a tree: " + (verticesCount - 1));

            return visitedCount == verticesCount && edgeCount == verticesCount - 1;
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
