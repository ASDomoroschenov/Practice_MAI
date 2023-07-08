namespace Task2;

public class IEqualityComparerImplementation<T>:
    IEqualityComparer<T>
{
    private static IEqualityComparerImplementation<T>? _comparer;

    public static IEqualityComparer<T> Instance
    {
        get
        {
            return _comparer ??= new IEqualityComparerImplementation<T>();
        }
    }

    public bool Equals(T? x, T? y)
    {
        if (x == null)
        {
            throw new ArgumentNullException(nameof(x));
        }

        if (y == null)
        {
            throw new ArgumentNullException();
        }

        return x.Equals(y);
    }

    public int GetHashCode(T? obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        return obj.GetHashCode();
    }
}