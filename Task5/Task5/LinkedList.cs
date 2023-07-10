namespace Task5;

using System.Collections;

public class LinkedList<T>:
    IEnumerable<T>,
    ICloneable,
    IEquatable<LinkedList<T>>
{

    public class LinkedListNode<TNode>
    {
        public LinkedListNode(
            TNode? initialData)
        {
            Data = initialData;
            Next = null;
        }

        public TNode? Data { get; set; }

        public LinkedListNode<TNode>? Next { get; set; }
    }

    private LinkedListNode<T>? _root;
    private int _length;

    public LinkedList()
    {
        _root = null;
        _length = 0;
    }

    public LinkedList(
        LinkedList<T>? obj)
    {
        if (ReferenceEquals(obj, null))
        {
            throw new ArgumentNullException(nameof(obj));
        }

        LinkedListNode<T> currentNode = null;
        LinkedListNode<T> prevNode = null;

        if (!ReferenceEquals(obj._root, null))
        {
            currentNode = obj._root;
            _root = new LinkedListNode<T>(obj._root.Data);

            while (!ReferenceEquals(currentNode, null))
            {
                if (!ReferenceEquals(prevNode, null))
                {
                    prevNode.Next = new LinkedListNode<T>(currentNode.Data);
                    prevNode = prevNode.Next;
                }
                else
                {
                    prevNode = _root;
                }

                currentNode = currentNode.Next;
            }

            _length = obj._length;
        }
    }

    public LinkedList(
        IEnumerable<T>? obj)
    {
        if (ReferenceEquals(obj, null))
        {
            throw new ArgumentNullException(nameof(obj));
        }

        LinkedListNode<T>? currentNode = _root;
        LinkedListNode<T>? prevNode = null;
        _length = 0;

        foreach (var itemObj in obj)
        {
            if (ReferenceEquals(_root, null))
            {
                _root = new LinkedListNode<T>(itemObj);
                currentNode = _root;
            }
            else
            {
                currentNode = new LinkedListNode<T>(itemObj);
            }

            if (!ReferenceEquals(prevNode, null))
            {
                prevNode.Next = currentNode;
            }

            prevNode = currentNode;
            _length++;
        }
    }

    public LinkedList<T> InsertFront(
        T? insertionData)
    {
        if (ReferenceEquals(_root, null))
        {
            _root = new LinkedListNode<T>(insertionData);
        }
        else
        {
            var oldRoot = _root;
            var newRoot = new LinkedListNode<T>(insertionData);

            _root = newRoot;
            _root.Next = oldRoot;
        }

        _length++;

        return this;
    }

    public LinkedList<T> InsertBack(
        T? insertionData)
    {
        if (ReferenceEquals(_root, null))
        {
            _root = new LinkedListNode<T>(insertionData);
        }
        else
        {
            LinkedListNode<T>? currentNode = _root;
            LinkedListNode<T>? prevNode = null;

            while (!ReferenceEquals(currentNode, null))
            {
                prevNode = currentNode;
                currentNode = currentNode.Next;
            }

            if (!ReferenceEquals(prevNode, null))
            {
                prevNode.Next = new LinkedListNode<T>(insertionData);
            }
        }

        _length++;

        return this;
    }

    public LinkedList<T> Insert(
        int insertionIndex,
        T? insertionData)
    {
        if ((ReferenceEquals(_root, null) && insertionIndex != 0) ||
            (insertionIndex > _length) ||
            (insertionIndex < 0))
        {
            throw new ArgumentException("Invalid index to insert    ", nameof(insertionIndex));
        }

        if (insertionIndex.Equals(0))
        {
            return InsertFront(insertionData);
        }

        if (insertionIndex.Equals(_length))
        {
            return InsertBack(insertionData);
        }

        LinkedListNode<T> currentNode = _root;
        LinkedListNode<T> prevNode = null;

        for (var i = 0; i < insertionIndex; i++)
        {
            prevNode = currentNode;
            currentNode = currentNode.Next;
        }

        LinkedListNode<T> insertionNode = new LinkedListNode<T>(insertionData);

        prevNode.Next = insertionNode;
        insertionNode.Next = currentNode;

        _length++;

        return this;
    }

    public LinkedList<T> RemoveFront()
    {
        if (ReferenceEquals(_root, null))
        {
            throw new OutOfMemoryException("Can't remove from empty LinkedList");
        }

        _root = _root.Next;
        _length--;

        return this;
    }

    public LinkedList<T> RemoveBack()
    {
        if (ReferenceEquals(_root, null))
        {
            throw new OutOfMemoryException("Can't remove from empty LinkedList");
        }

        LinkedListNode<T> currentNode = _root;
        LinkedListNode<T> prevNode = null;

        while (!ReferenceEquals(currentNode.Next, null))
        {
            prevNode = currentNode;
            currentNode = currentNode.Next;
        }

        prevNode.Next = null;
        _length--;

        return this;
    }

    public LinkedList<T> Remove(
        int removalIndex)
    {
        if (ReferenceEquals(_root, null))
        {
            throw new OutOfMemoryException("Can't remove from empty LinkedList");
        }

        if ((removalIndex >= _length) ||
            (removalIndex < 0))
        {
            throw new ArgumentException("Invalid index to remove", nameof(removalIndex));
        }

        if (removalIndex.Equals(0))
        {
            return RemoveFront();
        }

        if (removalIndex.Equals(_length - 1))
        {
            return RemoveBack();
        }

        LinkedListNode<T> currentNode = _root;
        LinkedListNode<T> prevNode = null;

        for (var i = 0; i < removalIndex; i++)
        {
            prevNode = currentNode;
            currentNode = currentNode.Next;
        }

        prevNode.Next = currentNode.Next;

        _length--;

        return this;
    }

    public LinkedList<T> Reverse()
    {
        if (ReferenceEquals(_root, null))
        {
            return this;
        }

        LinkedListNode<T> currentNode = _root;
        var reverseList = new List<T>();
        int indexReverseList = 0;

        while (!ReferenceEquals(currentNode, null))
        {
            reverseList.Add(currentNode.Data);
            currentNode = currentNode.Next;
        }

        reverseList.Reverse();
        LinkedList<T> newLinkedList = new LinkedList<T>(reverseList);

        return newLinkedList;
    }

    public static LinkedList<T> operator !(
        LinkedList<T> targetList)
    {
        return targetList.Reverse();
    }

    public static LinkedList<T> Concat(
        LinkedList<T>? firstList,
        LinkedList<T>? secondList)
    {
        LinkedList<T> resultList = new LinkedList<T>();
        LinkedListNode<T> prevNode = null;
        
        if (!ReferenceEquals(firstList, null) && !ReferenceEquals(firstList._root, null))
        {
            LinkedListNode<T> currentNode = firstList._root;

            resultList._root = new LinkedListNode<T>(firstList._root.Data);

            while (!ReferenceEquals(currentNode, null))
            {
                if (!ReferenceEquals(prevNode, null))
                {
                    prevNode.Next = new LinkedListNode<T>(currentNode.Data);
                    prevNode = prevNode.Next;
                }
                else
                {
                    prevNode = resultList._root;
                }

                currentNode = currentNode.Next;
            }

            resultList._length += firstList._length;
        }

        if (!ReferenceEquals(secondList, null) && !ReferenceEquals(secondList._root, null))
        {
            LinkedListNode<T> currentNode = secondList._root;

            resultList._root ??= new LinkedListNode<T>(secondList._root.Data);

            while (!ReferenceEquals(currentNode, null))
            {
                if (!ReferenceEquals(prevNode, null))
                {
                    prevNode.Next = new LinkedListNode<T>(currentNode.Data);
                    prevNode = prevNode.Next;
                }
                else
                {
                    prevNode = resultList._root;
                }

                currentNode = currentNode.Next;
            }

            resultList._length += secondList._length;
        }

        return resultList;
    }

    public static LinkedList<T> operator +(
        LinkedList<T> firstList,
        LinkedList<T> secondList)
    {
        return Concat(firstList, secondList);
    }

    private bool Contain(
        T value,
        IEqualityComparer<T> comparer)
    {
        LinkedListNode<T> currentNode = _root;

        while (!ReferenceEquals(currentNode, null))
        {
            if (comparer.Equals(currentNode.Data, value))
            {
                return true;
            }

            currentNode = currentNode.Next;
        }

        return false;
    }

    public static LinkedList<T> Intersection(
        LinkedList<T>? firstList,
        LinkedList<T>? secondList,
        IEqualityComparer<T>? comparer)
    {
        LinkedList<T> resultList = new LinkedList<T>();
        LinkedListNode<T> prevNode = null;
        
        if (ReferenceEquals(comparer, null))
        {
            throw new ArgumentNullException(nameof(comparer));
        }

        if (!ReferenceEquals(firstList, null) &&
            !ReferenceEquals(secondList, null))
        {
            LinkedListNode<T> currentNode = firstList._root;

            while (!ReferenceEquals(currentNode, null))
            {
                if (firstList.Contain(currentNode.Data, comparer) &&
                    secondList.Contain(currentNode.Data, comparer))
                {
                    if (resultList._root == null)
                    {
                        resultList._root = new LinkedListNode<T>(currentNode.Data);
                        prevNode = resultList._root;
                    }
                    else
                    {
                        prevNode.Next = new LinkedListNode<T>(currentNode.Data);
                        prevNode = prevNode.Next;
                    }

                    resultList._length++;
                }

                currentNode = currentNode.Next;
            }
        }
        else
        {
            return null;
        }

        return resultList;
    }

    public static LinkedList<T> operator &(
        LinkedList<T> firstList,
        LinkedList<T> secondList)
    {
        return Intersection(firstList, secondList, EqualityComparer<T>.Default);
    }

    public static LinkedList<T> ConcatWithoutDuplicate(
        LinkedList<T> firstList,
        LinkedList<T> secondList,
        IEqualityComparer<T> comparer)
    {
        LinkedList<T> resultList = new LinkedList<T>();
        LinkedListNode<T> prevNode = null;
        
        if (!ReferenceEquals(firstList, null) && !ReferenceEquals(firstList._root, null))
        {
            LinkedListNode<T> currentNode = firstList._root;

            while (!ReferenceEquals(currentNode, null))
            {
                if (!resultList.Contain(currentNode.Data, comparer))
                {
                    if (resultList._root == null)
                    {
                        resultList._root = new LinkedListNode<T>(currentNode.Data);
                        prevNode = resultList._root;
                    }
                    else
                    {
                        prevNode.Next = new LinkedListNode<T>(currentNode.Data);
                        prevNode = prevNode.Next;
                    }

                    resultList._length++;
                }

                currentNode = currentNode.Next;
            }
        }

        if (!ReferenceEquals(secondList, null) && !ReferenceEquals(secondList._root, null))
        {
            LinkedListNode<T> currentNode = secondList._root;

            while (!ReferenceEquals(currentNode, null))
            {
                if (!resultList.Contain(currentNode.Data, comparer))
                {
                    if (resultList._root == null)
                    {
                        resultList._root = new LinkedListNode<T>(currentNode.Data);
                        prevNode = resultList._root;
                    }
                    else
                    {
                        prevNode.Next = new LinkedListNode<T>(currentNode.Data);
                        prevNode = prevNode.Next;
                    }

                    resultList._length++;
                }

                currentNode = currentNode.Next;
            }
        }

        return resultList;   
    }

    public static LinkedList<T> operator |(
        LinkedList<T> firstList,
        LinkedList<T> secondList)
    {
        return ConcatWithoutDuplicate(firstList, secondList, EqualityComparer<T>.Default);
    }

    public static LinkedList<T> Subtraction(
        LinkedList<T>? firstList,
        LinkedList<T>? secondList,
        IEqualityComparer<T> comparer)
    {
        if (ReferenceEquals(firstList, null))
        {
            throw new ArgumentNullException(nameof(firstList));
        }

        if (ReferenceEquals(secondList, null))
        {
            throw new ArgumentNullException(nameof(secondList));
        }

        LinkedList<T> resultList = new LinkedList<T>();
        LinkedListNode<T> currentNode = firstList._root;
        LinkedListNode<T> prevNode = null;

        while (!ReferenceEquals(currentNode, null))
        {
            if (!secondList.Contain(currentNode.Data, comparer))
            {
                if (ReferenceEquals(resultList._root, null))
                {
                    resultList._root = new LinkedListNode<T>(currentNode.Data);
                    prevNode = resultList._root;
                }
                else
                {
                    prevNode.Next = new LinkedListNode<T>(currentNode.Data);
                    prevNode = prevNode.Next;
                }

                resultList._length++;
            }
            
            currentNode = currentNode.Next;
        }

        return resultList;
    }
    
    public static LinkedList<T> operator -(
        LinkedList<T> firstList,
        LinkedList<T> secondList)
    {
        return Subtraction(firstList, secondList, EqualityComparer<T>.Default);
    }

    private static T[] ToArray(
        LinkedList<T> list)
    {
        var array = new List<T>();
        LinkedListNode<T> currentNode = list._root;

        while (!ReferenceEquals(currentNode, null))
        {
            array.Add(currentNode.Data);
            currentNode = currentNode.Next;
        }

        return array.ToArray();
    }

    public void Sort(
        IComparer<T?>? comparer)
    {
        if (ReferenceEquals(comparer, null))
        {
            throw new ArgumentNullException(nameof(comparer));
        }

        var array = ToArray(this);

        SortMethods.Sort(
            array,
            SortMethods.SortingMode.Ascending,
            SortMethods.SortingAlgorithm.QuickSort,
            comparer);

        LinkedList<T> newList = new LinkedList<T>(array);
        _root = newList._root;
    }

    public void Sort(
        Comparer<T?>? comparer)
    {
        if (ReferenceEquals(comparer, null))
        {
            throw new ArgumentNullException(nameof(comparer));
        }

        var array = ToArray(this);

        SortMethods.Sort(
            array,
            SortMethods.SortingMode.Ascending,
            SortMethods.SortingAlgorithm.QuickSort,
            comparer);

        LinkedList<T> newList = new LinkedList<T>(array);
        _root = newList._root;
    }
    
    public void Sort(
        Comparison<T?>? comparer)
    {
        if (ReferenceEquals(comparer, null))
        {
            throw new ArgumentNullException(nameof(comparer));
        }

        var array = ToArray(this);

        SortMethods.Sort(
            array,
            SortMethods.SortingMode.Ascending,
            SortMethods.SortingAlgorithm.QuickSort,
            comparer);

        LinkedList<T> newList = new LinkedList<T>(array);
        _root = newList._root;
    }

    public void Apply(
        Action<T> action)
    {
        LinkedListNode<T> currentNode = _root;

        while (!ReferenceEquals(currentNode, null))
        {
            action(currentNode.Data);
            currentNode = currentNode.Next;
        }
    }

    public static bool operator ==(
        LinkedList<T>? firstList,
        LinkedList<T>? secondList)
    {
        if (ReferenceEquals(firstList, secondList))
        {
            return true;
        }

        if (ReferenceEquals(firstList, null) ||
            ReferenceEquals(secondList, null))
        {
            return false;
        }

        if (firstList._length != secondList._length)
        {
            return false;
        }

        LinkedListNode<T> currentNodeFirstList = firstList._root;
        LinkedListNode<T> currentNodeSecondList = secondList._root;

        while (!ReferenceEquals(currentNodeFirstList, null) &&
               !ReferenceEquals(currentNodeSecondList, null))
        {
            if (!currentNodeFirstList.Data.Equals(currentNodeSecondList.Data))
            {
                return false;
            }

            currentNodeFirstList = currentNodeFirstList.Next;
            currentNodeSecondList = currentNodeSecondList.Next;
        }

        return true;
    }

    public static bool operator !=(
        LinkedList<T> firstList,
        LinkedList<T> secondList)
    {
        return !(firstList == secondList);
    }
    
    public static LinkedList<T> operator *(
        LinkedList<T> firstList,
        LinkedList<T> secondList)
    {
        LinkedList<T> resultList = new LinkedList<T>();
        LinkedListNode<T> currentNodeFirstList = firstList._root;
        LinkedListNode<T> currentNodeSecondList = secondList._root;
        LinkedListNode<T> currentNodeResultList = null;

        while (!ReferenceEquals(currentNodeFirstList, null) &&
               !ReferenceEquals(currentNodeSecondList, null))
        {
            if (ReferenceEquals(currentNodeResultList, null))
            {
                resultList._root = new LinkedListNode<T>((dynamic?)currentNodeFirstList.Data * (dynamic?)currentNodeSecondList.Data);
                currentNodeResultList = resultList._root;
            }
            else
            {
                currentNodeResultList.Next = new LinkedListNode<T>((dynamic?)currentNodeFirstList.Data * (dynamic?)currentNodeSecondList.Data);
                currentNodeResultList = currentNodeResultList.Next;
            }

            currentNodeFirstList = currentNodeFirstList.Next;
            currentNodeSecondList = currentNodeSecondList.Next;
        }

        return resultList;
    }

    public override int GetHashCode()
    {
        int hashCode = 0;
        LinkedListNode<T> currentNode = _root;

        while (!ReferenceEquals(currentNode, null))
        {
            hashCode = HashCode.Combine(hashCode, currentNode.Data.GetHashCode());
            currentNode = currentNode.Next;
        }

        return hashCode;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        if (obj is LinkedList<T> objList)
        {
            return Equals(objList);
        }

        return false;
    }

    public override string ToString()
    {
        return $"[ {string.Join(", ", this)} ]";
    }

    public bool Equals(
        LinkedList<T>? other)
    {
        if (ReferenceEquals(other, null))
        {
            return false;
        }

        return this == other;
    }

    public object Clone()
    {
        LinkedList<T> cloneList = new LinkedList<T>();
        LinkedListNode<T> currentNode = _root;
        LinkedListNode<T> cloneListNode = null;

        while (!ReferenceEquals(currentNode, null))
        {
            if (ReferenceEquals(cloneList._root, null))
            {
                cloneList._root = new LinkedListNode<T>(currentNode.Data);
                cloneListNode = cloneList._root;
            }
            else
            {
                cloneListNode.Next = new LinkedListNode<T>(currentNode.Data);
                cloneListNode = cloneListNode.Next;
            }

            currentNode = currentNode.Next;
        }

        return cloneList;
    }

    public IEnumerator<T> GetEnumerator()
    {
        LinkedListNode<T> currentNode = _root;

        while (!ReferenceEquals(currentNode, null))
        {
            yield return currentNode.Data;
            currentNode = currentNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new Exception();
    }
    
}