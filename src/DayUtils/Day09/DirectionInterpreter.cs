using Advent22.DayUtils.Day09;
using Advent22.Utils;
using Advent22.Utils.Cardinals;

namespace Advent22.DaYUtils.DaY09;

// R 2 => (X:2,Y:0)
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
    
    public static Coordinate Apply (this Coordinate a, Coordinate b)
        => ( a.X + b.X , a.Y + b.Y);
    
    public static Coordinate Negate (this Coordinate coordinate)
        => (-coordinate.X, -coordinate.Y);

    public static Dictionary<XCardinalDir, Coordinate> GetAdjacentCoords(this Coordinate target)
    {
        return new Dictionary<XCardinalDir, Coordinate>()
        {
            [XCardinalDir.East]      = (target.X+1, target.Y+0),
            [XCardinalDir.West]      = (target.X-1, target.Y+0),
            
            [XCardinalDir.North]     = (target.X+0, target.Y+1),
            [XCardinalDir.South]     = (target.X+0, target.Y-1),
            
            [XCardinalDir.Northeast] = (target.X+1, target.Y+1),
            [XCardinalDir.Northwest] = (target.X-1, target.Y+1),
            
            [XCardinalDir.Southeast] = (target.X+1, target.Y-1),
            [XCardinalDir.Southwest] = (target.X-1, target.Y-1),
            
            [XCardinalDir.Center] = (target.X, target.Y)
        };
    }
    
    public static Dictionary<CardinalDir, Coordinate> GetCrossAdjacentCoords(this Coordinate target)
    {
        return new Dictionary<CardinalDir, Coordinate>
        {
            [CardinalDir.East]      = (target.X+1, target.Y+0),
            [CardinalDir.West]      = (target.X-1, target.Y+0),
            
            [CardinalDir.North]     = (target.X+0, target.Y+1),
            [CardinalDir.South]     = (target.X+0, target.Y-1)
        };
    }
}