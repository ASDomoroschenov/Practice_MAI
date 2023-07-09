namespace Task3
{

    public class Program
    {
        public static int ComparisonTest(
            int firstItem,
            int secondItem)
        {
            return firstItem - secondItem;
        }

        public class IntComparer :
            Comparer<int>
        {
            public override int Compare(
                int firstItem,
                int secondItem)
            {
                return firstItem.CompareTo(secondItem);
            }
        }

        public class IIntComparer :
            IComparer<int>
        {

            public int Compare(
                int firstItem,
                int secondItem)
            {
                return firstItem - secondItem;
            }

        }

        public static void Main(string[] args)
        {
            var collection = new[] { 3, 1, 2 };
            var comparerDelegate = new Comparison<int>((x, y) =>
            {
                return x.CompareTo(y);
            });
            
            collection.Sort(
                SortMethods.SortingMode.Ascending,
                SortMethods.SortingAlgorithm.MergeSort,
                comparerDelegate);
            
            Console.WriteLine($"[ { string.Join(", ", collection) } ]");
        }

    }

}