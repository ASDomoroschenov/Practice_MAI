namespace Task4;

public sealed class LeftBoxMethod:
    ICalculationIntegral
{

    public double Calculation(
        ICalculationIntegral.IntegralFunction? integralFunction,
        double lowerBound,
        double upperBound,
        int accuracy)
    {
        var sign = 1;
        if (integralFunction == null) throw new ArgumentNullException(nameof(integralFunction));
        if (accuracy <= 0) throw new ArgumentException("Invalid accuracy", nameof(accuracy));
        if (lowerBound.CompareTo(upperBound) == 0) return 0d;
        
        if (lowerBound.CompareTo(upperBound) > 0)
        {
            (lowerBound, upperBound) = (upperBound, lowerBound);
            sign = -1;
        }
        
        double sum = 0;
        double step = (upperBound - lowerBound) / accuracy;

        for(var i = 0; i < accuracy; ++i)
        {
            sum += integralFunction(lowerBound + i * step);
        }

        return sign * step * sum;
    }

    public string NameMethod => "LeftBoxMethod";

}