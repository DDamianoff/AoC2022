namespace Advent22.Day08.Utils;

internal partial class SquareGrid
{
    
    internal SquareGrid(string[] values)
    {
        _size = values.Length;
        
        if (values[0].Length != values.Length)
            throw new ArgumentException("Invalid grid");

        _grid = new int[_size,_size];

        for (var i = 0; i < values.Length; i++)
        {
            var value = values[i];
            var parsedString = value
                .Select(c =>
                    (int)char.GetNumericValue(c))
                .ToArray();

            for (var j = 0; j < parsedString.Length; j++) 
                _grid[i, j] = parsedString[j];
        }
    }
    
    private int[,] _grid;
    private readonly int _size;
    
    // thanks to this guy: https://stackoverflow.com/a/51241629
    public int[] ExtractColumn(int columnNumber) =>         // TODO: overload starting point to Pt2
        Enumerable
            .Range(0, _grid.GetLength(0))
            .Select(x => _grid[x, columnNumber])
            .ToArray();

    public int[] ExtractRow(int rowNumber) =>               // TODO: overload starting point to Pt2
        Enumerable
            .Range(0, _grid.GetLength(1))
            .Select(x => _grid[rowNumber, x])
            .ToArray();
}