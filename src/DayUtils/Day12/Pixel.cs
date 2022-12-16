namespace Advent22.DayUtils.Day12;

public struct Pixel
{
    public int Deep;
    //
    public bool HotSpot;
    public bool Indexed;

    public Pixel(int deep)
    {
        Deep = deep;
        HotSpot = false;
    }
    
    public Pixel()
    {
        Deep = 0;
        HotSpot = false;
    }
}