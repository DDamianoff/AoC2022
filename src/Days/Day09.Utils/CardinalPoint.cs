namespace Advent22.Day09.Utils;

public enum CardinalPoint
{
    South,
    North,
    West,
    East,
    
    Northeast,
    Southeast,
    Northwest,
    Southwest,
}

public static class CardinalPointHelper
{
    public static CardinalPoint Opposite(this CardinalPoint direction)
    {
        return direction switch
        {
            // thanks GTP
            CardinalPoint.East => CardinalPoint.West,
            CardinalPoint.West => CardinalPoint.East,
            CardinalPoint.North => CardinalPoint.South,
            CardinalPoint.South => CardinalPoint.North,
            
            CardinalPoint.Northeast => CardinalPoint.Southwest,
            CardinalPoint.Northwest => CardinalPoint.Southeast,
            CardinalPoint.Southeast => CardinalPoint.Northwest,
            CardinalPoint.Southwest => CardinalPoint.Northeast,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
        
    }
}