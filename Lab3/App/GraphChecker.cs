using System;

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

        // Метод для зчитування матриці суміжності з файлу
        private void ReadInput(string inputFile)
        {
            var lines = File.ReadAllLines(inputFile);
            verticesCount = int.Parse(lines[0]);
            adjacencyMatrix = new int[verticesCount, verticesCount];

            for (int i = 1; i <= verticesCount; i++)
            {
                var row = lines[i].Split(' ');
                for (int j = 0; j < verticesCount; j++)
                {
                    adjacencyMatrix[i - 1, j] = int.Parse(row[j]);
                }
            }
        }

        // Метод для перевірки, чи граф є деревом
        public bool IsTree()
        {
            bool[] visited = new bool[verticesCount];

            // Перевірка на цикли
            if (HasCycle(-1, 0, visited))
                return false;

            // Перевірка на зв'язність
            for (int i = 0; i < verticesCount; i++)
            {
                if (!visited[i])
                    return false;
            }

            return true;
        }

        // Перевірка на наявність циклів за допомогою DFS
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
