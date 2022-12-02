namespace AoC2022;

public static class Days
{
    public static void DayOne()
    {
        var rawValueList = File.ReadAllText("./inputs/day_one.txt");

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

    public static void DayTwo()
    {
        
    }
}