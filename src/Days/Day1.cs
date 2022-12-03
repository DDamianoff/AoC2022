using Advent22;

namespace Advent22;

public static class Day1
{
    public static void DayOne()
    {
        var rawValueList = File.ReadAllText("./inputs/D01.txt");

        var parsedElves = rawValueList
            .Split("\n\n")
            .Select(e => 
                e.Split("\n")
                    .Select(int.Parse)
                    .Sum())
            .Order()
            .TakeLast(3)
            .ToList();

        parsedElves
            .Last()
            .Display("Elf with most calories");

        parsedElves
            .Sum()
            .Display("Sum of top three elves with most calories");
    }
}