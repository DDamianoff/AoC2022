namespace Advent22;

internal static class Utils
{
    public static void Display(this object finalValue, string title) 
        => Console.WriteLine($"{title}: {finalValue}");

    public static void Times(this int times, Action acton) 
    {
        for (int i = 0; i < times; i++) 
            acton.Invoke();
    }
    
    
    public static bool InRange(this int @int, int maxRangeValue)
        => @int.InRange(0, maxRangeValue);
    
    public static bool InRange(this int @int, int minRangeValue, int maxRangeValue)
        => minRangeValue <= @int && @int <= maxRangeValue;
    
    public static bool InRange(this int @int, (int min, int max) range)
        => range.min <= @int && @int <= range.max;
    
    public static string Rotate(this string input)
    {
        var output = string.Empty;

        var aux  = input.Split("\n")
            // .Select(s => s.PadRight(maxLen, ' '))     4now assuming all are the same.
            .ToArray();
        
        // for (int i = 0; i < aux.Max(t => t.Length); i++)
        for (int i = 0; i < aux[0].Length; i++)
        {
            string line = string.Empty;
            foreach (var row in aux)
                line += row[i];
            output += new string(line.Reverse().ToArray());
            output += Environment.NewLine;
        }
        
        return output;
    }
}

