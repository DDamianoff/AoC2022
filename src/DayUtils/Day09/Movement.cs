namespace Advent22.DayUtils.Day09;

public readonly struct Movement
{
    private Movement(CardinalPoint direction, (int x, int y) relativeCoords, int count)
    {
        Direction = direction;
        RelativeCoords = relativeCoords;
        Count = count;
    }
    
    public CardinalPoint Direction 
    { get; }
    
    public (int x, int y) RelativeCoords 
    { get; }
    
    public int Count
    { get; }

    public static Movement Up(int count) => new Movement(CardinalPoint.North,   (0, +1), count);
    public static Movement Down(int count) => new Movement(CardinalPoint.South, (0, -1), count);
    public static Movement Right(int count) => new Movement(CardinalPoint.East, (+1, 0), count);
    public static Movement Left(int count) => new Movement(CardinalPoint.West,  (-1, 0), count);

    public override string ToString()
    {
        var asChar = new Dictionary<CardinalPoint, char> ()
        {
            [CardinalPoint.East]      = 'R',
            [CardinalPoint.West]      = 'L',
            [CardinalPoint.North]     = 'U',
            [CardinalPoint.South]     = 'D',
        };
        return $"={asChar[Direction]} {Count}=";
    }
}