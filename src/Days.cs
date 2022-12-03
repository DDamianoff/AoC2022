namespace AoC2022;

public static class Days
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

    public static void DayTwo()
    {
        var valueList = File
            .ReadAllText("./inputs/D02.txt")
            .Split("\n")
            .Select(e => 
                e.Replace(" ", ""))
            .Select(e => new
            {
                EnemyMovement = e[0],
                Strategy = e[1]
            })
            .ToList();
        
        valueList
            .Select(v =>
                string.Concat(
                    v.EnemyMovement,
                    v.Strategy.OperateStrategy()))
            .Select(s => 
                s.GetPoints() + 
                s.GetBonus())
            .Sum()
            .Display("Total (first)");

        valueList
            .Select(v =>
                string.Concat(
                    v.EnemyMovement,
                    v.Strategy.OperateStrategy(v.EnemyMovement)))
            .Select(s => 
                s.GetPoints() + 
                s.GetBonus())
            .Sum()
            .Display("Total (second)");
    }

    public static void DayThree()
    {
        var input = File.ReadAllLines("./inputs/D03.txt")
            .Select(backpack =>
            {
                var half = (backpack.Length / 2);
                return new[]
                {
                    backpack[0..half], backpack[half..]
                };
            });

        input
            .Select(bp =>
                bp[0].Distinct()
                    .First(c =>
                        bp[1].Distinct()
                            .Contains(c)))
            .Select(match =>
            {
                var asciiValue = Convert.ToByte(match);
                return new
                {
                    match,
                    priority = asciiValue < 91 // ASCII Table for reference.
                        ? asciiValue - 65 + 27
                        : asciiValue - 96
                };})
            .Sum(kv => kv.priority)
            .Display("total");
        
    }
}