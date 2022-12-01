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
            .ToList();

        parsedElves
            .Max()
            .Display("Elf with most calories");

        parsedElves
            .Order()
            .TakeLast(3)
            .Sum()
            .Display("Sum of top three elves with most calories");
    }
}