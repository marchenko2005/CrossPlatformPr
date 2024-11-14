using System;
using System.Linq;

namespace App
{
    public class Calculations
    {
        public long Factorial(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "Factorial is not defined for negative numbers.");
            }

            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        public long CalculatePermutations(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Input string cannot be null or empty.", nameof(input));
            }

            if (!input.All(char.IsLetter))
            {
                throw new ArgumentException("Input string must contain only letters.", nameof(input));
            }

            var letterCounts = input.GroupBy(c => c).Select(group => group.Count());
            long totalPermutations = Factorial(input.Length);

            foreach (var count in letterCounts)
            {
                totalPermutations /= Factorial(count);
            }

            return totalPermutations;
        }
    }
}
