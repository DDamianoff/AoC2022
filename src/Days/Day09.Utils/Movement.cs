namespace Advent22.Day09.Utils;

public readonly struct Movement
{
    public Movement(CardinalPoint direction, (int x, int y) relativeCoords)
    {
        Direction = direction;
        RelativeCoords = relativeCoords;
    }
    
    public CardinalPoint Direction 
    { get; }
    
    public (int x, int y) RelativeCoords 
    { get; }

    public static Movement Up(int count) => new Movement(CardinalPoint.North,   (0, +count));
    public static Movement Down(int count) => new Movement(CardinalPoint.South, (0, -count));
    public static Movement Right(int count) => new Movement(CardinalPoint.East, (+count, 0));
    public static Movement Left(int count) => new Movement(CardinalPoint.West,  (-count, 0));
}