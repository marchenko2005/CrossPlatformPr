namespace App
{
    internal static class Program
    {
        private static void Main()
        {
            string word;
            try
            {
                word = FileHandler.ReadWord();
                Console.WriteLine($"Word: {word}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            long permutationsCount;
            try
            {
                var calculations = new Calculations();
                permutationsCount = calculations.CalculatePermutations(word);
                Console.WriteLine($"Count of unique permutations: {permutationsCount}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            try
            {
                FileHandler.WriteResult(permutationsCount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
