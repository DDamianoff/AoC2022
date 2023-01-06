using System.Numerics;

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

    public static implicit operator Coordinate((int X, int Y) coordinate)
        => new(coordinate.X, coordinate.Y);

    public static implicit operator (int X, int Y)(Coordinate coordinate)
        => (coordinate.X, coordinate.Y);
    
    public static implicit operator Vector2(Coordinate coordinate)
        => new Vector2(coordinate.X, coordinate.Y);
    
    public static explicit operator Coordinate(Vector2 vector)
        => new ((int)vector.X, (int)vector.Y);
    
    #endregion

    #region Unary Operators

    public static Coordinate operator +(Coordinate coordinate)
        => new(+coordinate.X, +coordinate.Y);

    public static Coordinate operator -(Coordinate coordinate)
        => new(-coordinate.X, -coordinate.Y);

    #endregion

    #region Arithmethic operators

    public static Coordinate operator +(Coordinate left, Coordinate right)
        => new(left.X + right.X, right.Y + left.Y);

    public static Coordinate operator -(Coordinate left, Coordinate right)
        => new(left.X - right.X, right.Y - left.Y);

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
        => left.X == right.X && left.Y == right.Y;

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
    
    /// <summary>
    /// Corresponding coordinates of the objects that surronds this object:
    /// left, up, right, down.
    /// </summary>
    /// <returns>
    /// an array of coordinates
    /// </returns>
    /// <remarks>
    /// This WON'T check if the adjacent coordinates exists
    /// in the working grid.
    /// </remarks>
    public Coordinate[] AdjacentCross()
    {
        return new[]
        {
            this + Direction.Up(),
            this + Direction.Down(),
            this + Direction.Left(),
            this + Direction.Right()
        };
    }
    
    /// <summary>
    /// returns the surrounding coordinates of this object,
    /// including diagonals and the center.
    /// </summary>
    /// <param name="radius">
    /// radius of coordinates. Default is UGH just
    /// TODO: tell GTP to complete this after done.
    /// </param>
    /// <returns></returns>
    public Coordinate[] CompleteAdjacentArea(int radius = 1)
    {
        throw new NotImplementedException();
    }

    public Direction EstimatedDirectionTo(Coordinate another)
    {
        var dirAsRelativeCoor = this - another;
        var xMovement = dirAsRelativeCoor.X != 0;
        var yMovement = dirAsRelativeCoor.Y != 0;

        switch (xMovement)
        {
            // x
            case true when !yMovement:
            {
                var count = Math.Abs(dirAsRelativeCoor.X) > 1
                    ? Math.Abs(dirAsRelativeCoor.Y) - 1
                    : 1;
            
                return dirAsRelativeCoor.X > 0
                    ? Direction.Right(count)
                    : Direction.Left(count);
            }
            
            // y
            case false when yMovement:
            {
                var count = Math.Abs(dirAsRelativeCoor.Y) > 1
                    ? Math.Abs(dirAsRelativeCoor.Y) - 1
                    : 1;
            
                return dirAsRelativeCoor.Y > 0
                    ? Direction.Up(count)
                    : Direction.Down(count);
            }
            
            // x,y
            case true when true:
                throw new NotImplementedException("diagonal directions not supported");
            
            // none
            case false when true:
                return Direction.Center();
        }
    }
}