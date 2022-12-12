namespace Advent22.DayUtils.Day08;

internal partial class SquareGrid
{
    
    internal SquareGrid(string[] values)
    {
        _size = values.Length;
        
        if (values[0].Length != values.Length)
            throw new ArgumentException("Invalid grid");

        _gridV2 = new Dictionary<(int row, int col), int>();

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
    private readonly Dictionary<(int row, int col), int> _gridV2;
    private readonly int _size;
}