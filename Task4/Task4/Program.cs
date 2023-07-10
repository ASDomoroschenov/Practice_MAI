namespace Task4
{

    class Program
    {

        public static double Function(
            double x)
        {
            return 1 + x;
        }

        static void Main(string[] args)
        {
            var integralMethods = new ICalculationIntegral[]
            {
                new LeftBoxMethod(),
                new RightBoxMethod(),
                new MiddleRectangleMethod(),
                new TrapezoidalMethod(),
                new SimpsonMethod()
            };

            foreach (var method in integralMethods)
            {
                Console.WriteLine($"{method.NameMethod} = {method.Calculation(x => 1 + x, 1, 10, 1000)}");
            }
        }

    }

}