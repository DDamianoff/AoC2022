namespace Advent22.Utils.Cardinals;

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

    #region Parsing operators

    public static implicit operator Coordinate ((int X, int Y) coordinate) 
        => new (coordinate.X, coordinate.Y);

    public static implicit operator (int X, int Y) (Coordinate coordinate) 
        => (coordinate.X, coordinate.Y);

    #endregion

    #region Unary Operators

    public static Coordinate operator +(Coordinate coordinate) 
        => new (+coordinate.X, +coordinate.Y);

    public static Coordinate operator -(Coordinate coordinate) 
        => new (-coordinate.X, -coordinate.Y);

    #endregion
    
    #region Arithmethic operators

    public static Coordinate operator +(Coordinate left, Coordinate right)
        => new (left.X + right.X, right.Y + left.Y);
    
    public static Coordinate operator -(Coordinate left, Coordinate right)
        => new (left.X - right.X, right.Y - left.Y);
    
    public static Coordinate operator ++(Coordinate coordinate)
    {
        coordinate.X++;
        coordinate.Y++;
        
        return coordinate;
    }

    public static Coordinate operator --(Coordinate coordinate)
    {
        coordinate.X--;
        coordinate.Y--;
        
        return coordinate;
    }

    #endregion

    #region Boolean operators
    
    public static bool operator ==(Coordinate left, Coordinate right) 
        => left.X == right.X && left.Y == right.Y ;

    public static bool operator !=(Coordinate left, Coordinate right) 
        => !(left == right);
    
    public static bool operator >(Coordinate left, Coordinate right) 
        => left.X > right.X && left.Y > right.Y;

    public static bool operator <(Coordinate left, Coordinate right)
        => left.X < right.X && left.Y < right.Y;
    
    public static bool operator >=(Coordinate left, Coordinate right) 
        => left.X > right.X && left.Y > right.Y;

    public static bool operator <=(Coordinate left, Coordinate right)
        => left.X <= right.X && left.Y <= right.Y;
    
    public bool Equals(Coordinate other) 
        => X == other.X && Y == other.Y;

    public override bool Equals(object obj) 
        => obj is Coordinate other && Equals(other);
    
   #endregion
   
    public override int GetHashCode() 
        => HashCode.Combine(X, Y);

    public override string ToString() 
        => $"({X},{Y})";
}