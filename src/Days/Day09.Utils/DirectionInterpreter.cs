namespace Advent22.Day09.Utils;

// R 2 => (x:2,y:0)
public static class DirectionInterpreter
{
    public static Movement GetSingleFromStringCommand(string command)
    {
        
        var directionChar = command[0];
        var count = (int)char.GetNumericValue(command[2]);

        return directionChar switch
        {
            'R' => Movement.Right(count),
            'L' => Movement.Left(count),
            'U' => Movement.Up(count),
            'D' => Movement.Down(count),
            _ => throw new InvalidOperationException()
        };
    }
    
    public static Movement GetSingle(CardinalPoint direction)
    {
        return direction switch
        {
            CardinalPoint.East => Movement.Right(1),
            CardinalPoint.West => Movement.Left(1),
            CardinalPoint.North => Movement.Up(1),
            CardinalPoint.South => Movement.Down(1),
            _ => throw new InvalidOperationException()
        };
    }

    public static IEnumerable<Movement> GetMultipleFromStringCommand(string[] commands) 
        => commands.Select(GetSingleFromStringCommand);
    
    public static (int x, int y) Operate (this (int x, int y) a, (int x, int y) b)
        => ( a.x + b.x , a.y + b.y);
    
    public static (int x, int y) Negate (this (int x, int y) coordinate)
        => (-coordinate.x, -coordinate.y);

    public static Dictionary<CardinalPoint, (int x, int y)> GetAdjacentCoords(this (int x, int y) target)
    {
        return new Dictionary<CardinalPoint, (int x, int y)>()
        {
            [CardinalPoint.East]      = (target.x+1, target.y+0),
            [CardinalPoint.West]      = (target.x-1, target.y+0),
            
            [CardinalPoint.North]     = (target.x+0, target.y+1),
            [CardinalPoint.South]     = (target.x+0, target.y-1),
            
            [CardinalPoint.Northeast] = (target.x+1, target.y+1),
            [CardinalPoint.Northwest] = (target.x-1, target.y+1),
            
            [CardinalPoint.Southeast] = (target.x+1, target.y-1),
            [CardinalPoint.Southwest] = (target.x-1, target.y-1),
            
            [CardinalPoint.Center] = (target.x, target.y)
        };
    }
}