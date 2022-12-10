namespace Advent22.Day08.Utils;

internal partial class SquareGrid
{
    
    internal SquareGrid(string[] values)
    {
        _size = values.Length;
        
        if (values[0].Length != values.Length)
            throw new ArgumentException("Invalid grid");

        _gridV2 = new Dictionary<(int x, int y), int>();

        for (var i = 0; i < values.Length; i++)
        {
            var value = values[i];
            var parsedString = value
                .Select(c =>
                    (int)char.GetNumericValue(c))
                .ToArray();

            for (var j = 0; j < parsedString.Length; j++) 
                _gridV2[(i, j)] = parsedString[j];
        }
    }
    private readonly Dictionary<(int x, int y), int> _gridV2;
    private readonly int _size;
    

    public int[] ExtractColumn(int columnNumber) =>
        Enumerable
            .Range(0, _size)
            .Select(x => _gridV2[(x, columnNumber)])
            .ToArray();

    public int[] ExtractRow(int rowNumber) =>
        Enumerable
            .Range(0, _size)
            .Select(y => _gridV2[(rowNumber, y)])
            .ToArray();
}