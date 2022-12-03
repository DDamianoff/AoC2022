using System.Collections;
using System.Runtime.InteropServices.ComTypes;

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

    public static void DayThreePartOne()
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

    public static void DayThreePartTwo()
    {
        // I'll take an imperative approach with this part
        var input = new Stack<string>(File.ReadAllLines("./inputs/D03.txt"));
        
        var grouped = new List<int>();

        const int groupSize = 3;
        while (input.Count != 0)
        {
            var elfGroup = new string[groupSize];

            for (int i = 0; i < groupSize; i++)
            {
                elfGroup[i] = input.Pop();
            }

            var sticker = elfGroup.GetSticker();

            var stickers = string.Join("", elfGroup).Count(c => c == sticker);

            var priority = sticker.PriorityValue() * stickers;
            
            grouped.Add(priority);
        }

        grouped.ForEach(Console.WriteLine);
    }

    internal static char GetSticker(this string[] list)
    {
        var matcher = new Dictionary<char, int>();
        var processedList = "";

        foreach (var backpack in list)
        {
            processedList += String.Join("",backpack.Distinct());
        }
        
        foreach (char c in processedList)
        {
            if (matcher.ContainsKey(c))
                matcher[c]++;
            else 
                matcher[c] = 0;
        }
        return matcher.First(c => c.Value == 2).Key;
    }
    
    internal static int PriorityValue (this char c)
    {
        var asciiValue = Convert.ToByte(c);
        return asciiValue < 91 // ASCII Table for reference.
            ? asciiValue - 65 + 27
            : asciiValue - 96;
    }
}