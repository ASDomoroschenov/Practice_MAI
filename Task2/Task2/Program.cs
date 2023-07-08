namespace Task2
{

    class Program
    {
        static void Main(string[] args)
        {
            var permutations = ExtendedEnumerable.GenerateCombinations(
                new List<int>() { 1, 2, 3 },
                2,
                IEqualityComparerImplementation<int>.Instance);

            foreach (var permutation in permutations)
            {
                Console.WriteLine("[" + string.Join(", ", permutation) + "]");
            }
        }

    }

}