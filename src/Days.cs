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
        var valueList = File
            .ReadAllText("./inputs/day_two.txt")
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

}