using Advent22.Utils.Cardinals;

namespace Advent22.DayUtils.Day12;

public struct Pixel
{
    public int Deep;
    public Coordinate Location;
    public char Character;

    public Pixel(int deep, Coordinate location, char character)
    {
        Deep = deep;
        Location = location;
        Character = character;
    }
    
    public Pixel() => Deep = 0;

    public override string ToString()
    {
        return $"{Character}" + $" {Deep}".PadLeft(2, '0');
    }
}