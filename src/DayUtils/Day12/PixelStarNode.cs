#nullable enable
using Advent22.Utils.Cardinals;

namespace Advent22.DayUtils.Day12;

public class PixelStarNode
{

    public PixelStarNode(Pixel pixel, 
        PixelStarNode parent, 
        double cost, 
        double targetDistance)
    {
        _pixel = pixel;
        Parent = parent;
        Cost = cost;
        TargetDistance = targetDistance;
    }
    
    public PixelStarNode(Pixel pixel)
    {
        _pixel = pixel;
        Parent = null;
        Cost = int.MaxValue;
        TargetDistance = int.MaxValue;
    }

    private readonly Pixel _pixel;

    public int Deep => _pixel.Deep;

    public Coordinate Location => _pixel.Location;
    
    public PixelStarNode? Parent 
    { get; set; }
    
    public double Cost 
    { get; set; }
    
    public double TargetDistance 
    { get; set; }

    public double TotalCost => Cost + TargetDistance;

    public bool PreviouslyExpanded;
    
    public Direction Direction = Direction.Center();
    
    public bool CanMoveTo(PixelStarNode starNode)
    {
        var anotherDeep = starNode.Deep;

        if (anotherDeep == Deep)
            return true;
        
        if (anotherDeep <= Deep)
            return true;

        if (anotherDeep == Deep + 1)
            return true;
        
        return false;
    }

    public override string ToString()
    {
        var locationString = string.Empty;
        
        locationString += "(";
        locationString += $"{Location.X}".PadLeft(3, '0');
        locationString += ",";
        locationString += $"{Location.Y}".PadLeft(3, '0');
        locationString += ")";
        
        return $"{_pixel} {locationString}";
    }
}