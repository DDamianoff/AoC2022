using Advent22.DayUtils.Day12;
using Advent22.Utils.Cardinals;

namespace Advent22;

public static class Day12
{
    public static void DayTwelve()
    {
        var rawInput = File.ReadAllLines("./inputs/D02.txt");
        var gridV2 = new Dictionary<Coordinate, Pixel>();
        var parser = GetCharCardinalSetter();
        
        var startPoint = new Coordinate();
        var endPoint = new Coordinate();
        
        for (int i = 0; i < rawInput.Length; i += 3)
        for (int j = 0; j < rawInput[i].Length; i += 3)
        { }
    }

    private static Dictionary<char, int> GetCharCardinalSetter(int startFrom = 0, 
        char startFlag = 'S',
        char endFlag = 'E')
    {
        var parser = new Dictionary<char, int>
        {
            [startFlag] = int.MinValue,
            [endFlag] = int.MaxValue
        };

        for (var i = 'a'; i < 'z'; i++)
            parser[i] = i - '0' - startFrom;
        
        return parser;
    }
    
}

