namespace Advent22.Utils.Cardinals;

public static class CardinalHelper
{
    public static Dictionary<XCardinalDir, Coordinate> GetAdjacentCoords(this Coordinate target)
    {
        return new Dictionary<XCardinalDir, Coordinate>()
        {
            [XCardinalDir.East]      = (target.X+1, target.Y+0),
            [XCardinalDir.West]      = (target.X-1, target.Y+0),
            
            [XCardinalDir.North]     = (target.X+0, target.Y+1),
            [XCardinalDir.South]     = (target.X+0, target.Y-1),
            
            [XCardinalDir.Northeast] = (target.X+1, target.Y+1),
            [XCardinalDir.Northwest] = (target.X-1, target.Y+1),
            
            [XCardinalDir.Southeast] = (target.X+1, target.Y-1),
            [XCardinalDir.Southwest] = (target.X-1, target.Y-1),
            
            [XCardinalDir.Center] = (target.X, target.Y)
        };
    }
    
    public static Dictionary<CardinalDir, Coordinate> GetCrossAdjacentCoords(this Coordinate target)
    {
        return new Dictionary<CardinalDir, Coordinate>
        {
            [CardinalDir.East]      = (target.X+1, target.Y+0),
            [CardinalDir.West]      = (target.X-1, target.Y+0),
            
            [CardinalDir.North]     = (target.X+0, target.Y+1),
            [CardinalDir.South]     = (target.X+0, target.Y-1)
        };
    }
}