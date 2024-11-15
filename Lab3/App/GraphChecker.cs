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
            LoadGraphFromFile(inputFile);
        }

        // Метод для завантаження графа з файлу
        private void LoadGraphFromFile(string inputFile)
        {
            Console.WriteLine("Зчитування даних з файлу...");
            string[] lines = File.ReadAllLines(inputFile);

            if (lines.Length == 0)
                throw new FormatException("Файл  порожній.");
            ParseVerticesCount(lines[0]);
            ParseAdjacencyMatrix(lines);
            DisplayAdjacencyMatrix();
        }

        // Зчитування кількості вершин графа
        private void ParseVerticesCount(string firstLine)
        {
            if (!int.TryParse(firstLine.Trim(), out verticesCount) || verticesCount <= 0)
                throw new FormatException("Перший рядок має містити додатнє ціле число — кількість вершин графа.");
            Console.WriteLine("Кількість вершин графа: " + verticesCount);
            adjacencyMatrix = new int[verticesCount, verticesCount];
        }

        // Зчитування та перевірка матриці суміжності
        private void ParseAdjacencyMatrix(string[] lines)
        {
            for (int i = 0; i < verticesCount; i++)
            {
                string[] row = lines[i + 1].Trim().Split(' ');
                if (row.Length != verticesCount)
                    throw new FormatException("Довжина рядка матриці не відповідає кількості вершин графа.");

                for (int j = 0; j < verticesCount; j++)
                {
                    if (!int.TryParse(row[j], out adjacencyMatrix[i, j]) || (adjacencyMatrix[i, j] != 0 && adjacencyMatrix[i, j] != 1))
                        throw new FormatException("Матриця суміжності має містити лише 0 або 1.");
                }
            }
        }

        // Відображення матриці суміжності
        private void DisplayAdjacencyMatrix()
        {
            Console.WriteLine("Матриця суміжності зчитана успішно:");
            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    Console.Write(adjacencyMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Перевірка, чи є граф деревом
        public bool IsTree()
        {
            return !HasCycle() && IsConnected() && HasCorrectEdgeCount();
        }

        // Перевірка наявності циклів
        private bool HasCycle()
        {
            Console.WriteLine("Перевірка на цикли...");
            bool[] visited = new bool[verticesCount];
            if (DetectCycle(-1, 0, visited))
            {
                Console.WriteLine("У графі знайдено цикл.");
                return true;
            }
            return false;
        }

        // Рекурсивний пошук циклів
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

        // Перевірка зв'язності графа
        private bool IsConnected()
        {
            Console.WriteLine("Перевірка зв'язності графа...");
            bool[] visited = new bool[verticesCount];
            DepthFirstSearch(0, visited);

            int visitedCount = CountVisitedVertices(visited);
            Console.WriteLine("Кількість відвіданих вершин: " + visitedCount);
            return visitedCount == verticesCount;
        }

        // Глибокий пошук для зв'язності
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

        // Підрахунок відвіданих вершин
        private int CountVisitedVertices(bool[] visited)
        {
            int count = 0;
            foreach (bool v in visited)
            {
                if (v) count++;
            }
            return count;
        }

    }
}
