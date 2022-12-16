namespace Advent22.Utils.Cardinals;

/// <summary>
/// Representation of a relative coordinate
/// </summary>
public struct Direction
{
    private readonly XCardinalDir _direction;

    public int Times
    { get; set; }

    private Direction(XCardinalDir direction, int times = 1)
    {
        _direction = direction;
        Times = times;
    }

    public Direction()
    {
        _direction = XCardinalDir.Center;
        Times = 1;
    }
    
    #region Builders
    
    // the cursed one
    private static Direction Center(int times = 1)
        => new(XCardinalDir.Center, times);
    
    public static Direction Up(int times = 1)
        => new(XCardinalDir.North, times);

    public static Direction Down(int times = 1)
        => new(XCardinalDir.South, times);

    public static Direction Left(int times = 1)
        => new(XCardinalDir.West, times);

    public static Direction Right(int times = 1)
        => new(XCardinalDir.East, times);
    
    
    public static Direction North(int times = 1)
        => new(XCardinalDir.North, times);

    public static Direction South(int times = 1)
        => new(XCardinalDir.South, times);

    public static Direction West(int times = 1)
        => new(XCardinalDir.West, times);

    public static Direction East(int times = 1)
        => new(XCardinalDir.East, times);

    
    public static Direction Northeast(int times = 1)
        => new(XCardinalDir.Northeast, times);
    
    public static Direction Northwest(int times = 1)
        => new(XCardinalDir.Northwest, times);
    
    public static Direction Southeast(int times = 1)
        => new(XCardinalDir.Southeast, times);
    
    public static Direction Southwest(int times = 1)
        => new(XCardinalDir.Southeast, times);
    
    #endregion

    public Coordinate AsRelativeCoordinate() 
        => this;

    private enum XCardinalDir
    {
        // ordered by unicode arrows
        Center = -1,
        
        West = 0,
        North = 1,
        East = 2,
        South = 3,
    
        Northwest = 6,
        Northeast = 7,
        Southeast = 8,
        Southwest = 9
    }
    public char AsCharArrow() 
        => _direction == XCardinalDir.Center 
            ? '\u2605' 
            : (char)('\u2190' + _direction);

    
    public static Direction operator !(Direction direction)
    {
        return direction._direction switch
        {
            XCardinalDir.East       =>  West(),
            XCardinalDir.West       =>  East(),
            XCardinalDir.North      =>  South(),
            XCardinalDir.South      =>  North(),
            XCardinalDir.Northeast  =>  Southwest(),
            XCardinalDir.Northwest  =>  Southeast(),
            XCardinalDir.Southeast  =>  Northwest(),
            XCardinalDir.Southwest  =>  Northeast(),
            XCardinalDir.Center     =>  Center(),
            _                       =>  throw new InvalidOperationException()
        };
    }

    public static implicit operator Coordinate(Direction direction)
    {
        return direction._direction switch
        {
            XCardinalDir.West        =>  (-direction.Times, 0),
            XCardinalDir.East        =>  (+direction.Times, 0),
            
            XCardinalDir.North       =>  (0, +direction.Times),
            XCardinalDir.South       =>  (0, -direction.Times),
            
            XCardinalDir.Northwest   =>  (-direction.Times, +direction.Times),
            XCardinalDir.Northeast   =>  (+direction.Times, +direction.Times),
            
            XCardinalDir.Southwest   =>  (-direction.Times, -direction.Times),
            XCardinalDir.Southeast   =>  (+direction.Times, -direction.Times),
            
            XCardinalDir.Center      =>  (0, 0),
            
            _                        =>  throw new InvalidOperationException(message:"💀")
        };
    }
}