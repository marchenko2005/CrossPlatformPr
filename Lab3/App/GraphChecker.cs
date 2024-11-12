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

        // Метод для зчитування матриці суміжності з файлу у форматі "3 010101010"
        private void ReadInput(string inputFile)
        {
            Console.WriteLine("Читання вхідних даних...");

            var line = File.ReadAllText(inputFile).Trim();
            var parts = line.Split(' ');

            // Перевірка коректності формату
            if (parts.Length != 2)
                throw new FormatException("Вхідний файл повинен містити два частини: кількість вершин і матрицю суміжності.");

            // Зчитуємо кількість вершин
            if (!int.TryParse(parts[0], out verticesCount))
                throw new FormatException("Перша частина повинна бути цілим числом, яке представляє кількість вершин.");

            Console.WriteLine("Кількість вершин: " + verticesCount);

            // Створюємо матрицю суміжності
            adjacencyMatrix = new int[verticesCount, verticesCount];

            // Зчитуємо рядок з 0 та 1 і заповнюємо матрицю
            string matrixData = parts[1];
            if (matrixData.Length != verticesCount * verticesCount)
                throw new FormatException("Рядок матриці суміжності не відповідає очікуваній довжині.");

            int index = 0;
            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    adjacencyMatrix[i, j] = matrixData[index] - '0'; // Перетворення символу на число
                    index++;
                }
            }

            Console.WriteLine("Матриця суміжності:");
            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    Console.Write(adjacencyMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Метод для перевірки, чи граф є деревом
        public bool IsTree()
        {
            bool[] visited = new bool[verticesCount];

            Console.WriteLine("Перевірка на цикли...");

            // Перевірка на цикли
            if (HasCycle(-1, 0, visited))
            {
                Console.WriteLine("Виявлено цикл у графі.");
                return false;
            }

            Console.WriteLine("Перевірка на зв'язність...");

            // Перевірка на зв'язність
            for (int i = 0; i < verticesCount; i++)
            {
                if (!visited[i])
                {
                    Console.WriteLine("Граф не є зв'язним.");
                    return false;
                }
            }

            Console.WriteLine("Граф є зв'язним і не містить циклів.");
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
