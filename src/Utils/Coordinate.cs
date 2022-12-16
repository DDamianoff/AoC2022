namespace Advent22.Utils;

public struct Coordinate
{
    public int X;
    public int Y;

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public Coordinate()
    {
        X = 0;
        Y = 0;
    }

    public static implicit operator Coordinate ((int X, int Y) coordinate) 
        => new (coordinate.X, coordinate.Y);

    public static implicit operator (int X, int Y)(Coordinate coordinate) 
        => (coordinate.X, coordinate.Y);
}