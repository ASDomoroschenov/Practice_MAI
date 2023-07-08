using System.Collections;

public static class ExtendedEnumerable
{

    public static IEnumerable<IEnumerable<T>> GenerateCombinations<T>(
        IEnumerable<T>? iterableObject,
        int k,
        IEqualityComparer<T>? comparer)
    {
        if (iterableObject == null)
        {
            throw new ArgumentNullException(nameof(iterableObject), "Argument must be a not null");
        }

        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(iterableObject), "Argument must be a not null");
        }

        if (!CheckEquality(iterableObject, comparer))
        {
            throw new ArgumentException("In iterable object there is duplicate", nameof(iterableObject));
        }
        
        return GetCombinations(iterableObject, new List<T>(), k, 0);
    }

    private static IEnumerable<IEnumerable<T>> GetCombinations<T>(
        IEnumerable<T> iterableObject,
        List<T> combinations,
        int k,
        int startPosition)
    {
        List<IEnumerable<T>> resultList = new List<IEnumerable<T>>();

        if (combinations.Count == k)
        {
            resultList.Add(combinations.ToList());
        }
        else
        {
            for (var i = startPosition; i < iterableObject.Count(); i++)
            {
                combinations.Add(iterableObject.ElementAt(i));
                resultList.AddRange(GetCombinations(iterableObject, combinations, k, i));
                combinations.RemoveAt(combinations.Count - 1);
            }
        }

        return resultList;
    }

    #region Subsets
    
    public static IEnumerable<IEnumerable<T>> GenerateSubsets<T>(
        IEnumerable<T>? iterableObject,
        IEqualityComparer<T>? comparer)
    {
        if (iterableObject == null)
        {
            throw new ArgumentNullException(nameof(iterableObject), "Argument must be a not null");
        }

        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(iterableObject), "Argument must be a not null");
        }

        if (!CheckEquality(iterableObject, comparer))
        {
            throw new ArgumentException("In iterable object there is duplicate", nameof(iterableObject));
        }

        return GetSubsets(iterableObject);
    }

    private static IEnumerable<IEnumerable<T>> GetSubsets<T>(
        IEnumerable<T> iterableObject)
    {
        var subsets = new List<List<T>>() { new() };
        
        foreach (var itermIterableObject in iterableObject)
        {
            var currentSubset = new List<List<T>>();

            foreach (var itemSubset in subsets)
            {
                currentSubset.Add(new List<T>(itemSubset) { itermIterableObject });
            }
            
            subsets.AddRange(currentSubset);
        }
        
        return subsets;
    }
    
    #endregion

    #region Permutations
    
    public static IEnumerable<IEnumerable<T>> GeneratePermutations<T>(
        IEnumerable<T>? iterableObject,
        IEqualityComparer<T>? comparer)
    {
        if (iterableObject == null)
        {
            throw new ArgumentNullException(nameof(iterableObject), "Argument must be a not null");
        }

        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(iterableObject), "Argument must be a not null");
        }
        
        if (!CheckEquality(iterableObject, comparer))
        {
            throw new ArgumentException("In iterable object there is duplicate", nameof(iterableObject));
        }

        return GetPermutations(iterableObject);
    }

    private static IEnumerable<IEnumerable<T>> GetPermutations<T>(
        IEnumerable<T> iterableObject)
    {
        List<IEnumerable<T>> resultList = new List<IEnumerable<T>>();

        if (iterableObject.Count() == 1)
        {
            return new List<IEnumerable<T>>() { iterableObject };
        }

        for (int i = 0; i < iterableObject.Count(); i++)
        {
            var element = iterableObject.ElementAt(i);
            var remainingElements = iterableObject.Take(i).Concat(iterableObject.Skip(i + 1));
            var permutationsOfRemaining = GetPermutations<T>(remainingElements);

            foreach (var permutation in permutationsOfRemaining)
            {
                resultList.Add(new List<T>() { element }.Concat(permutation));
            }
        }

        return resultList;
    }
    
    #endregion

    private static bool CheckEquality<T>(
        IEnumerable<T> iterableObject,
        IEqualityComparer<T> comparer)
    {
        var uniqueElements = new HashSet<T>(comparer);

        foreach (var item in iterableObject)
        {
            if (!uniqueElements.Add(item))
            {
                return false;
            }
        }

        return true;
    }

}