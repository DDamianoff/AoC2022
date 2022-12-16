using Advent22.Utils;

namespace Advent22;

public static class Day3
{
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
                elfGroup[i] = input.Pop();
            
            var priority = elfGroup
                .GetSticker()
                .PriorityValue();
            
            grouped.Add(priority);
        }

        grouped
            .Sum()
            .Display("Result (part two)");
    }

    private static char GetSticker(this string[] list)
    {
        var matcher = new Dictionary<char, int>();
        
        var processedList = list.Aggregate("", (current, next) 
            => current + string.Join("", next.Distinct()));

        foreach (char c in processedList)
            if (matcher.ContainsKey(c))
                matcher[c]++;
            else
                matcher[c] = 0;
        return matcher.First(c => c.Value == 2).Key;
    }

    private static int PriorityValue (this char c)
    {
        var asciiValue = Convert.ToByte(c);
        return asciiValue < 91 // ASCII Table for reference.
            ? asciiValue - 65 + 27
            : asciiValue - 96;
    }
}