namespace Advent22.Utils.Cardinals;
// TODO: pending refactor: transform this into "Direction" struct.
[Obsolete("use Utils.Cardinals.Direction")]
public enum CardinalDir
{
    South,
    North,
    West,
    East
}

[Obsolete("use Utils.Cardinals.Direction")]
public enum XCardinalDir
{
    South,
    North,
    West,
    East,
    
    Northeast,
    Southeast,
    Northwest,
    Southwest,
    
    Center,
}

[Obsolete("use Utils.Cardinals.Direction")]
public static class CardinalPointHelper
{
    public static XCardinalDir Opposite(this XCardinalDir direction)
    {
        return direction switch
        {
            // thanks GTP
            XCardinalDir.East => XCardinalDir.West,
            XCardinalDir.West => XCardinalDir.East,
            XCardinalDir.North => XCardinalDir.South,
            XCardinalDir.South => XCardinalDir.North,
            
            XCardinalDir.Northeast => XCardinalDir.Southwest,
            XCardinalDir.Northwest => XCardinalDir.Southeast,
            XCardinalDir.Southeast => XCardinalDir.Northwest,
            XCardinalDir.Southwest => XCardinalDir.Northeast,
            XCardinalDir.Center => XCardinalDir.Center,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
    
    public static CardinalDir Opposite(this CardinalDir direction)
    {
        return direction switch
        {
            // thanks GTP
            CardinalDir.East => CardinalDir.West,
            CardinalDir.West => CardinalDir.East,
            CardinalDir.North => CardinalDir.South,
            CardinalDir.South => CardinalDir.North,

            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}