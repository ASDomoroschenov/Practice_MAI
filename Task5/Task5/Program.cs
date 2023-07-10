namespace Task5
{

    class Program
    {

        static void Main(string[] args)
        {
            var firstList = new LinkedList<int>(new List<int>() { 1, 2, 3 });
            var secondList = new LinkedList<int>(new List<int>() { 1, 5, 6 });
            
            var firstListReverse = !firstList;
            Console.WriteLine($"!{firstList.ToString()} = {firstListReverse.ToString()}");

            var concatList = firstList + secondList;
            Console.WriteLine($"{firstList.ToString()} + {secondList.ToString()} = {concatList.ToString()}");

            var intersectionList = firstList & secondList;
            Console.WriteLine($"{firstList.ToString()} & {secondList.ToString()} = {intersectionList.ToString()}");

            var concatWithoutDuplicateList = firstList | secondList;
            Console.WriteLine($"{firstList.ToString()} | {secondList.ToString()} = {concatWithoutDuplicateList.ToString()}");

            var subtractionList = firstList - secondList;
            Console.WriteLine($"{firstList.ToString()} - {secondList.ToString()} = {subtractionList.ToString()}");
            
            Console.WriteLine($"{firstList.ToString()} == {secondList.ToString()} - {firstList == secondList}");
            Console.WriteLine($"{firstList.ToString()} == {firstList.ToString()} - {firstList == firstList}");

            var multList = firstList * secondList;
            Console.WriteLine($"{firstList.ToString()} * {secondList.ToString()} = {multList.ToString()}");
        }

    }

}