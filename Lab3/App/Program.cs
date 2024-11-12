using System;
using System.IO;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Встановлюємо кодування UTF-8 для коректного відображення українських символів
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Визначаємо кореневу папку проєкту
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;

            // Формуємо шлях до файлів у папці проекту
            string inputFilePath = Path.Combine(projectRoot, "INPUT.TXT");
            string outputFilePath = Path.Combine(projectRoot, "OUTPUT.TXT");

            Console.WriteLine("Зчитування даних з файлу: " + inputFilePath);

            try
            {
                // Створення об'єкта GraphChecker та перевірка графа
                GraphChecker graphChecker = new GraphChecker(inputFilePath);
                string result = graphChecker.IsTree() ? "YES" : "NO";

                Console.WriteLine("Результат перевірки: " + (result == "YES" ? "Граф є деревом" : "Граф не є деревом"));
                Console.WriteLine("Запис результату у файл: " + outputFilePath);

                // Запис результату у файл
                File.WriteAllText(outputFilePath, result);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Помилка: Файл не знайдено за шляхом " + inputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Виникла помилка: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}
