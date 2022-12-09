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
}