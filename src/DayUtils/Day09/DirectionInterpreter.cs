using Advent22.DayUtils.Day09;
using Advent22.Utils;
using Advent22.Utils.Cardinals;

namespace Advent22.DayUtils.DaY09;

// R 2 => (X:2,Y:0)
[Obsolete("use new a shiny Cardinal.Direction instead")]
public static class DirectionInterpreter
{
    public static Movement GetSingleFromStringCommand(string command)
    {
        
        var directionChar = command[0];
        var count = int.Parse(command[2..]);

        return directionChar switch
        {
            'R' => Movement.Right(count),
            'L' => Movement.Left(count),
            'U' => Movement.Up(count),
            'D' => Movement.Down(count),
            _ => throw new InvalidOperationException()
        };
    }
    
    public static Movement GetSingle(CardinalDir direction)
    {
        return direction switch
        {
            CardinalDir.East => Movement.Right(1),
            CardinalDir.West => Movement.Left(1),
            CardinalDir.North => Movement.Up(1),
            CardinalDir.South => Movement.Down(1),
            _ => throw new InvalidOperationException()
        };
    }

    public static IEnumerable<Movement> GetMultipleFromStringCommand(string[] commands) 
        => commands.Select(GetSingleFromStringCommand);
    
    [Obsolete("use defined operators instead")]
    public static Coordinate Apply (this Coordinate a, Coordinate b)
        => ( a.X + b.X , a.Y + b.Y);
    
    [Obsolete("use defined operators instead")]
    public static Coordinate Negate (this Coordinate coordinate)
        => (-coordinate.X, -coordinate.Y);

    // moved to CardinalHelper
}