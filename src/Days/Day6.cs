namespace Advent22;

public static class Day6
{
    public static void DaySix()
    {
        var input = File.ReadAllText("./inputs/D06.txt");;
        
        input
            .FindFirstIndex()
            .Display("First package index");
        
        input
            .FindFirstMessageIndex()
            .Display("First message index");
        
    }

    private static int FindFirstIndex(this string input)
    {
        var buffer = new CircularArray<char>(4);
        var comparer = new ArrayComparer<char>();
        
        for (int i = 0; i < input.Length; i++)
        {
            buffer.Push(input[i]);
            
            if (i < 3)
                continue;
            
            if (!comparer.AllDifferent(buffer.ToArray())) 
                continue;
            
            return i + 1;
        }
        throw new InvalidOperationException();
    }
    private static int FindFirstMessageIndex(this string input)
    {
        var buffer = new CircularArray<char>(14);
        var comparer = new ArrayComparer<char>();
        
        for (int i = 0; i < input.Length; i++)
        {
            buffer.Push(input[i]);
            
            if (i < 3)
                continue;
            
            if (!comparer.AllDifferent(buffer.ToArray())) 
                continue;
            
            return i + 1;
        }
        throw new InvalidOperationException();
    }
}


// TODO: separate Fixed array from CircularArray (CircularArray.FixedArray)
internal readonly struct CircularArray<T>
{
    public CircularArray(int size)
    {
        _size = size;
        _buffer = new T [_size];
    }
    
    private readonly int _size;
    private readonly T[] _buffer;

    public int Count => _size;
    
    public void Push(T item)
    {
        for (int i = _size - 2; i >= 0; i--) 
            _buffer[i + 1] = _buffer[i];
        _buffer[0] = item;
    }

    public void Clear()
    {
        for (int i = 0; i < _size; i++) 
            _buffer[i] = default;
    }

    public bool Contains(T item) => _buffer.Contains(item);

    public T this[int index]
    {
        get => _buffer[Circle(index)];
        set => _buffer[Circle(index)] = value;
    }
    
    // TODO: assign a behavior to this
    public T[] this[Range range] => throw
        new NotImplementedException();

    private int Circle(int index) => index % _size;

    public List<T> ToList() => _buffer.ToList();
    
    public T[] ToArray() => _buffer;

    public override string ToString()
    {
        var outValue = string.Empty;
        for (int i = _buffer.Length - 1; i >= 0; i--)
            outValue += _buffer[i];
        
        return outValue;
    }
}

public readonly struct ArrayComparer<T>
{
    public ArrayComparer()
    {
        
    }
    // TODO: imperative approach using upper triangular
    public bool AllEqual(T[] input) 
        => input.All(e => Equals(e, input[0]));

    public bool AllDifferent(T[] input) 
        => input.Distinct().Count() == input.Length;

    // TODO: add other cases
}

