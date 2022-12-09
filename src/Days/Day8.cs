using Advent22.Day08.Utils;

namespace Advent22;

public static class Day8
{
    public static void DayEight()
    {
        var input = File.ReadAllLines("./inputs/D0X.txt");
            // .Select(s => s
            //     .ToCharArray()
            //     .Select(c => 
            //         (int)char.GetNumericValue(c))
            //     .ToArray()
            // )
            // .ToArray();
            
        var grid = new Grid(input);

    }
}