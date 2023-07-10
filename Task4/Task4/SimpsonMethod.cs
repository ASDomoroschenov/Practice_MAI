namespace Task4;

public class SimpsonMethod:
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
        
        var step = (upperBound - lowerBound) / accuracy;
        var sum1 = 0d;
        var sum2 = 0d;
        
        for (var k = 1; k <= accuracy; k++)
        {
            var xk = lowerBound + k * step;
            
            if (k <= accuracy - 1)
            {
                sum1 += integralFunction(xk);
            }

            sum2 += integralFunction((xk + lowerBound + (k - 1) * step) / 2);
        }

        return sign * (step / 3d * (1d / 2d * integralFunction(lowerBound) + sum1 + 2 * sum2 +
                             1d / 2d * integralFunction(upperBound)));
    }

    string ICalculationIntegral.NameMethod => "SimpsonMethod";

}