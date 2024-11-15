namespace Lab2
{
    public static class LabyrinthProcessor

    {
        public static int ProcessLabyrinth(string input)
        {
            ValidateInputNotEmpty(input);

            var (gridSize, steps, grid) = ParseInput(input);

            ValidateGridSize(gridSize);
            ValidateStepCount(steps);
            ValidateGrid(grid, gridSize);

            return CalculatePaths(gridSize, steps, grid);
        }

        public static void ValidateInputNotEmpty(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Вхідний файл порожній або містить лише пробіли.");
        }

        public static (int gridSize, int steps, char[,] grid) ParseInput(string input)
        {
            string[] lines = input.Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length < 2)
                throw new ArgumentException("Неправильний формат вхідних даних: недостатньо рядків.");

            string[] firstLine = lines[0].Split();
            if (firstLine.Length != 2)
                throw new ArgumentException("Неправильний формат вхідних даних: перший рядок повинен містити два числа.");

            if (!int.TryParse(firstLine[0], out int gridSize) || !int.TryParse(firstLine[1], out int steps))
                throw new ArgumentException("Неправильний формат вхідних даних: розмір лабіринту та кількість кроків повинні бути цілими числами.");

            char[,] grid = new char[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                string line = lines[i + 1];
                if (line.Length != gridSize)
                    throw new ArgumentException($"Неправильний формат вхідних даних: рядок {i + 2} повинен містити {gridSize} символів.");

                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = line[j];
                }
            }

            return (gridSize, steps, grid);
        }

        public static void ValidateGridSize(int gridSize)
        {
            if (gridSize <= 0)
                throw new ArgumentException("Розмір лабіринту повинен бути додатнім.");
        }

        public static void ValidateStepCount(int steps)
        {
            if (steps < 0)
                throw new ArgumentException("Кількість кроків не може бути негативною.");
        }

        public static void ValidateGrid(char[,] grid, int gridSize)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[i, j] != '0' && grid[i, j] != '1')
                        throw new ArgumentException($"Неправильний формат вхідних даних: символ у рядку {i + 1}, колонка {j + 1} повинен бути '0' або '1'.");
                }
            }
        }

        public static int CalculatePaths(int gridSize, int steps, char[,] grid)
        {
            int[,,] paths = new int[2, gridSize + 2, gridSize + 2];
            paths[0, 1, 1] = 1;

            for (int step = 1; step <= steps; step++)
            {
                for (int i = 0; i <= gridSize + 1; i++)
                {
                    for (int j = 0; j <= gridSize + 1; j++)
                    {
                        paths[step % 2, i, j] = 0;
                    }
                }

                for (int i = 1; i <= gridSize; i++)
                {
                    for (int j = 1; j <= gridSize; j++)
                    {
                        if (grid[i - 1, j - 1] == '0')
                        {
                            paths[step % 2, i, j] = paths[(step - 1) % 2, i - 1, j]
                                                  + paths[(step - 1) % 2, i + 1, j]
                                                  + paths[(step - 1) % 2, i, j - 1]
                                                  + paths[(step - 1) % 2, i, j + 1];
                        }
                    }
                }
            }
            return paths[steps % 2, gridSize, gridSize];
        }
    }
}
