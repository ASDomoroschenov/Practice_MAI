namespace Task4;

public interface ICalculationIntegral
{
    
    public delegate double IntegralFunction(
        double x);

    public double Calculation(
        IntegralFunction? function,
        double lowerBound,
        double upperBound,
        int accuracy);

    public string NameMethod
    {
        get;
    }

}