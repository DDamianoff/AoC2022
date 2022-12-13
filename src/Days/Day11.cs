
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Advent22.DayUtils.Day11;

namespace Advent22;

public static class Day11
{
    public static void DayEleven()
    {
        var input = File.ReadAllText("./inputs/D11.txt").Split("\n\n").ToList();
        var apes = new List<Ape>();
        
        input.ForEach(s => apes.Add(new Ape(s, apes)));

        20.Times(() => apes.ForEach(a => a.PlayMonkeyTurn()));

        apes.ForEach(a => a.Display("result"));
        
        Console.WriteLine();

        apes
            .Select(a => a.InspectedTimes)
            .OrderDescending()
            .Take(2)
            .Aggregate((current, next) => current * next)
            .Display("Ape business level");
    }
}