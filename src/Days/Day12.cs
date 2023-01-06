using Advent22.DayUtils.Day12;
using Advent22.Utils;
using Advent22.Utils.Cardinals;

namespace Advent22;

public static class Day12
{
    public static void DayTwelve()
    {
        var rawInput = File.ReadAllLines("./inputs/D12.txt");
        var gridV2 = new Dictionary<Coordinate, Pixel>();
        var parser = GetCharCardinalSetter();

        var startPoint = new Coordinate();
        var endPoint = new Coordinate();

        var Widght = rawInput.Length;
        var Height = rawInput[0].Length;
        
        for (int i = 0; i < rawInput.Length; i++)
        for (int j = 0; j < rawInput[i].Length; j++)
        {
            var x = j;
            var y = rawInput.Length - 1 - i;
            Coordinate current = (x, y);
            var pixelChar = rawInput[i][j];
            var deep = parser[pixelChar];
            
            gridV2[current] = new Pixel(deep, current, pixelChar);

            if (pixelChar == 'S')
                startPoint = current;
            
            if (pixelChar == 'E')
                endPoint = current;
        }
        
        var searcher = new StarSearcher(gridV2, startPoint, endPoint);

        var success = searcher.PerformSearch();
        
        if (!success)
            throw new Exception("Didn't find any path");

        var result = searcher.BuildPath();
        
        result
            .Count
            .Display("Shortest path count");
        
        gridV2.DisplayPathVisualization(result,Widght, Height);
    }

    private static Dictionary<char, int> GetCharCardinalSetter(char startFlag = 'S',
        char endFlag = 'E')
    {
        var parser = new Dictionary<char, int>
        {
            [startFlag] = AsCardinal('a'),
            [endFlag] = AsCardinal('z')
        };

        for (var i = 'a'; i <= 'z'; i++) 
            parser[i] = AsCardinal(i);

        int AsCardinal(char c) => c - 'a';

        return parser;
    }

    private static void DisplayPathVisualization(
        this Dictionary<Coordinate, Pixel> grid, 
        Stack<PixelStarNode> path,
        int wight,
        int height)
    {
        for (int y = 0; y < wight; y++)
        {
            for (int x = 0; x < height; x++)
            {
                var current = grid[(x, wight - 1 - y)];
                
                var color = current.Deep switch
                {
                    0 => ConsoleColor.Black,
                    1 => ConsoleColor.DarkCyan,
                    2 => ConsoleColor.DarkBlue,
                    _ => (ConsoleColor)(current.Deep % 16)
                };

                if (color == ConsoleColor.Yellow)
                    color = ConsoleColor.Black;
                
                Console.BackgroundColor = color;
                
                if (path.Any(p => p.Location == (x, wight - 1 - y)))
                    PrintAsPathChar(path.First(p => p.Location == (x, wight - 1 - y)).Direction.AsCharArrow());
                else
                    PrintAsNormalChar(current.Character, color);
                
                Console.ResetColor();
            }

            Console.Write("\n");
        }
        
        void PrintAsPathChar(char c)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(c);
        }
        
        void PrintAsNormalChar(char c, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(c);
        }
    }
    
}

