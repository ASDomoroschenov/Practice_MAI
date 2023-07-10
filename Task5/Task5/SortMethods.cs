namespace Task5;

public static class SortMethods
{

    public enum SortingMode
    {
        Ascending,
        Descending
    }

    public enum SortingAlgorithm
    {
        InsertionSort,
        SelectionSort,
        Heapsort,
        QuickSort,
        MergeSort
    }

    public static void Sort<T>(
        this T?[]? collection,
        SortingMode sortingMode,
        SortingAlgorithm sortingAlgorithm)
        where T : IComparable<T>
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        var comparerDelegate = new Comparison<T?>((x, y) =>
        {
            if (ReferenceEquals(x, null)) return -1;
            if (ReferenceEquals(y, null)) return 1;
            if (ReferenceEquals(x, null) && ReferenceEquals(y, null)) return 0;
            if (!ReferenceEquals(x, null) && !ReferenceEquals(y, null)) return x.CompareTo(y);
            
            return 0;
        });

        WrapperSortAlgorithm(
            collection,
            sortingMode,
            sortingAlgorithm,
            comparerDelegate);
    }

    public static void Sort<T>(
        this T?[]? collection,
        SortingMode sortingMode,
        SortingAlgorithm sortingAlgorithm,
        IComparer<T?>? comparer)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(comparer));
        }
        
        WrapperSortAlgorithm(
            collection,
            sortingMode,
            sortingAlgorithm,
            comparer.Compare);
    }

    public static void Sort<T>(
        this T?[]? collection,
        SortingMode sortingMode,
        SortingAlgorithm sortingAlgorithm,
        Comparer<T?>? comparer)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(comparer));
        }
        
        WrapperSortAlgorithm(
            collection,
            sortingMode,
            sortingAlgorithm,
            comparer.Compare);
    }

    public static void Sort<T>(
        this T[] collection,
        SortingMode sortingMode,
        SortingAlgorithm sortingAlgorithm,
        Comparison<T?>? comparer)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(comparer));
        }
        
        WrapperSortAlgorithm(
            collection,
            sortingMode,
            sortingAlgorithm,
            comparer);
    }

    private static void WrapperSortAlgorithm<T>(
        T[] collection,
        SortingMode sortingMode,
        SortingAlgorithm sortingAlgorithm,
        Comparison<T> comparer)
    {
        switch (sortingAlgorithm)
        { 
            case SortingAlgorithm.InsertionSort:
                InsertionSort(
                    collection,
                    sortingMode,
                    comparer);
                break;
            case SortingAlgorithm.SelectionSort:
                SelectionSort(
                    collection,
                    sortingMode,
                    comparer);
                break;
            case SortingAlgorithm.Heapsort:
                Heapsort(
                    collection,
                    sortingMode,
                    comparer);
                break;
            case SortingAlgorithm.QuickSort:
                QuickSort(
                    collection,
                    sortingMode,
                    comparer);
                break;
            case SortingAlgorithm.MergeSort:
                MergeSort(
                    collection,
                    sortingMode,
                    comparer);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sortingAlgorithm), sortingAlgorithm, "Invalid sorting algorithm type");
        }
    }

    #region SortingAlgorithms

    #region InsertionSort
    
    private static void InsertionSort<T>(
        T[] collection,
        SortingMode sortingMode,
        Comparison<T> comparer)
    {
        var comparerIndex = sortingMode == SortingMode.Ascending ? 1 : -1;
       
        for (var i = 1; i < collection.Length; i++)
        {
            for (var j = i; j > 0 && comparer(collection[j - 1], collection[j]) == comparerIndex; --j)
            {
                (collection[j - 1], collection[j]) = (collection[j], collection[j - 1]);
            }
        }
    }
    
    #endregion

    #region SelectionSort
    
    private static void SelectionSort<T>(
        T[] collection,
        SortingMode sortingMode,
        Comparison<T> comparer)
    {
        var comparerIndex = sortingMode == SortingMode.Ascending ? -1 : 1;
        
        for (var i = 0; i < collection.Length - 1; ++i)
        {
            var target = i;

            for (var j = i + 1; j < collection.Length; ++j)
            {
                if (comparer(collection[j], collection[target]) == comparerIndex)
                {
                    target = j;   
                }
                
                (collection[i], collection[target]) = (collection[target], collection[i]);   
            }
        }
    }
    
    #endregion

    #region HeapSort

    private static void Heapsort<T>(
        T[] collection,
        SortingMode sortingMode,
        Comparison<T> comparer)
    {
        var comparerIndex = sortingMode == SortingMode.Ascending ? 1 : -1;

        for (var i = collection.Length / 2 - 1; i >= 0; --i)
        {
            Heapify(collection, collection.Length, i, comparer, comparerIndex);   
        }

        for (var i = collection.Length - 1; i >= 0; --i)
        {
            (collection[0], collection[i]) = (collection[i], collection[0]);
            Heapify(collection, i, 0, comparer, comparerIndex);
        }
    }
    
    private static void Heapify<T>(
        T[] collection,
        int length,
        int index,
        Comparison<T> comparer,
        int comparerIndex)
    {
        var target = index;
        var left = 2 * index + 1;
        var right = 2 * index + 2;

        if (left < length &&
            comparer(collection[left], collection[target]) == comparerIndex)
        {
            target = left;   
        }

        if (right < length &&
            comparer(collection[right], collection[target]) == comparerIndex)
        {
            target = right;   
        }

        if (target == index)
        {
            return;
        }

        (collection[index], collection[target]) = (collection[target], collection[index]);

        Heapify(collection, length, target, comparer, comparerIndex);
    }

    #endregion

    #region QuickSort
    
    private static void QuickSort<T>(
        T[] collection,
        SortingMode sortingMode,
        Comparison<T> comparer)
    {
        var comparerIndex = sortingMode == SortingMode.Ascending ? -1 : 1;
        
        QuickSortRec(collection, 0, collection.Length - 1, comparer, comparerIndex);
    }
    
    private static void QuickSortRec<T>(
        T[] collection,
        int minIndex,
        int maxIndex,
        Comparison<T> comparer,
        int comparerIndex)
    {
        if (minIndex >= maxIndex)
        {
            return;
        }

        var pivotIndex = Partition(collection, minIndex, maxIndex, comparer, comparerIndex);
        
        QuickSortRec(collection, minIndex, pivotIndex - 1, comparer, comparerIndex);
        QuickSortRec(collection, pivotIndex + 1, maxIndex, comparer, comparerIndex);
    }
    
    private static int Partition<T>(
        T[] collection,
        int minIndex,
        int maxIndex,
        Comparison<T> comparer,
        int comparerIndex)
    {
        var pivot = minIndex - 1;
        
        for (var i = minIndex; i < maxIndex; ++i)
        {
            if (comparer(collection[i], collection[maxIndex]) == comparerIndex)
            {
                pivot++;
                (collection[pivot], collection[i]) = (collection[i], collection[pivot]);
            }   
        }
        
        pivot++;
        (collection[pivot], collection[maxIndex]) = (collection[maxIndex], collection[pivot]);
        
        return pivot;
    }
    
    #endregion

    #region MergeSort
    
    private static void MergeSort<T>(
        T[] collection,
        SortingMode sortingMode,
        Comparison<T> comparer)
    {
        var comparerIndex = sortingMode == SortingMode.Ascending ? -1 : 1;
        MergeSortRecursion(collection, 0, collection.Length - 1, comparer, comparerIndex);
    }
    
    private static void MergeSortRecursion<T>(
        T[] collection,
        int lowIndex,
        int highIndex,
        Comparison<T> comparer,
        int comparerIndex)
    {
        if (lowIndex >= highIndex)
        {
            return;
        }

        var middleIndex = (lowIndex + highIndex) / 2;
        
        MergeSortRecursion(collection, lowIndex, middleIndex, comparer, comparerIndex);
        MergeSortRecursion(collection, middleIndex + 1, highIndex, comparer, comparerIndex);
        Merge(collection, lowIndex, middleIndex, highIndex, comparer, comparerIndex);
    }
    
    private static void Merge<T>(
        T[] collection,
        int lowIndex,
        int middleIndex,
        int highIndex,
        Comparison<T> comparer,
        int comparerIndex)
    {
        var left = lowIndex;
        var right = middleIndex + 1;
        var tempArr = new T[highIndex - lowIndex + 1];
        var index = 0;

        while (left <= middleIndex && right <= highIndex)
        {
            if (comparer(collection[left], collection[right]) == comparerIndex)
            {
                tempArr[index] = collection[left];
                left++;
            }
            else
            {
                tempArr[index] = collection[right];
                right++;
            }
            
            index++;
        }

        for (var i = left; i <= middleIndex; i++)
        {
            tempArr[index] = collection[i];
            index++;
        }

        for (var i = right; i <= highIndex; i++)
        {
            tempArr[index] = collection[i];
            index++;
        }

        for (var i = 0; i < tempArr.Length; i++)
        {
            collection[lowIndex + i] = tempArr[i];   
        }
    }
    
    #endregion

    #endregion
}