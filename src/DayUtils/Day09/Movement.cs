using Advent22.Utils;
using Advent22.Utils.Cardinals;

namespace Advent22.DayUtils.Day09;

public readonly struct Movement
{
    private Movement(CardinalDir direction, (int x, int y) relativeCoords, int count)
    {
        Direction = direction;
        RelativeCoords = relativeCoords;
        Count = count;
    }
    
    public CardinalDir Direction 
    { get; }
    
    public (int x, int y) RelativeCoords 
    { get; }
    
    public int Count
    { get; }

    public static Movement Up(int count = 1) => new(CardinalDir.North,   (0, +1), count);
    public static Movement Down(int count = 1) => new(CardinalDir.South, (0, -1), count);
    public static Movement Right(int count = 1) => new(CardinalDir.East, (+1, 0), count);
    public static Movement Left(int count = 1) => new(CardinalDir.West,  (-1, 0), count);

    public override string ToString()
    {
        var asChar = new Dictionary<CardinalDir, char> ()
        {
            [CardinalDir.East]      = 'R',
            [CardinalDir.West]      = 'L',
            [CardinalDir.North]     = 'U',
            [CardinalDir.South]     = 'D',
        };
        return $"={asChar[Direction]} {Count}=";
    }
}