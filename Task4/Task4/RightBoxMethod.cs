namespace Task4;

public class RightBoxMethod:
    ICalculationIntegral
{
    
    double ICalculationIntegral.Calculation(
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
        
        double step = (upperBound - lowerBound) / accuracy;
        double sum = 0;
        
        for (var i = 1; i <= accuracy; i++)
        {
            sum += integralFunction(lowerBound + i * step);
        }

        return sign * step * sum;
    }

    string ICalculationIntegral.NameMethod => "RightBoxMethod";

}