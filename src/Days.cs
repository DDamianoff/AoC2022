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
        var input = File
            .ReadAllText("./inputs/day_two.txt")
            .Split("\n");
        // this should be an enum.
        var match = new
        {
            Win = 6,
            Draw = 3,
            Loose = 0
        };
        
        var bonus = new Dictionary<char, int>()
        {
            ['X'] = 1,
            ['Y'] = 2,
            ['Z'] = 3
        };
        
        input
            .Select(strategy => strategy switch
            {
                "A Y" or "B Z" or "C X" => match.Win,
                "A X" or "B Y" or "C Z" => match.Draw,
                "A Z" or "B X" or "C Y" => match.Loose,
                _ => throw new Exception("Shouldn't happen but makes the Roslyn happy")
            } + bonus[strategy[2]])
            .Sum()
            .Display("Total (first part)");
    }
}