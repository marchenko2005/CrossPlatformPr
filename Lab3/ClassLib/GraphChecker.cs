using System;
using System.IO;

namespace ClassLib
{
    public class GraphChecker
    {
        private int[,] adjacencyMatrix;
        private int verticesCount;

        public GraphChecker(string inputFilePath)
        {
            LoadGraphFromFile(inputFilePath);
        }

        private void LoadGraphFromFile(string inputFile)
        {
            Console.WriteLine("Зчитування даних з файлу...");
            string[] lines = File.ReadAllLines(inputFile);
            if (lines.Length == 0)
                throw new FormatException("Файл порожній.");
            ParseVerticesCount(lines[0]);
            ParseAdjacencyMatrix(lines);
        }

        private void ParseVerticesCount(string firstLine)
        {
            if (!int.TryParse(firstLine.Trim(), out verticesCount) || verticesCount <= 0)
                throw new FormatException("Перший рядок має містити кількість вершин графа.");
            adjacencyMatrix = new int[verticesCount, verticesCount];
        }

        private void ParseAdjacencyMatrix(string[] lines)
        {
            for (int i = 0; i < verticesCount; i++)
            {
                string[] row = lines[i + 1].Split(' ');
                if (row.Length != verticesCount)
                    throw new FormatException("Кількість елементів у рядку не відповідає кількості вершин графа.");
                for (int j = 0; j < verticesCount; j++)
                {
                    if (!int.TryParse(row[j], out adjacencyMatrix[i, j]) || (adjacencyMatrix[i, j] != 0 && adjacencyMatrix[i, j] != 1))
                        throw new FormatException("Матриця суміжності має містити лише 0 та 1.");
                }
            }
        }

        public bool IsTree()
        {
            return !HasCycle() && IsConnected() && HasCorrectEdgeCount();
        }

        private bool HasCycle()
        {
            bool[] visited = new bool[verticesCount];
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
                        if (DetectCycle(vertex, i, visited)) return true;
                    }
                    else if (i != parent) return true;
                }
            }
            return false;
        }

        private bool IsConnected()
        {
            bool[] visited = new bool[verticesCount];
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
                    if (adjacencyMatrix[i, j] == 1) edgeCount++;
                }
            }
            return edgeCount == verticesCount - 1;
        }
    }
}
