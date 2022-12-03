namespace AoC2022;

internal static class Utils
{
    public static void Display(this object finalValue, string title) 
        => Console.WriteLine($"{title}: {finalValue}");

    public static void Times(this int times, Action acton) 
    {
        for (int i = 0; i < times; i++) 
            acton.Invoke();
    }
}

internal static class DayTwoUtils
{
    
    internal static int GetBonus(this string match) => match[1] switch
    {
        'P' => 1,
        'Q' => 2,
        'R' => 3,
        _ => throw new Exception("Shouldn't happen but makes the Roslyn happy")
    };
    
    internal static int GetPoints(this string match) =>
        match switch
        {
            "AQ" or "BR" or "CP" => 6,
            "AP" or "BQ" or "CR" => 3,
            "AR" or "BP" or "CQ" => 0,
            _ => throw new Exception("Shouldn't happen but makes the Roslyn happy")
        };

    internal static char OperateStrategy(this char movement) => 
        movement switch
        {
            'X' => 'P',
            'Y' => 'Q',
            'Z' => 'R',
            _ => throw new Exception()
        };

    internal static char OperateStrategy(this char movement, char enemyMovement)
    {
        // TODO: rotating values
        // var values =  new [] { 'A', 'B', 'C' };
        
        var draw = new Dictionary<char, char>
        {
            ['A'] = 'P',
            ['B'] = 'Q',
            ['C'] = 'R'
        };
        
        var lose = new Dictionary<char, char>
        {
            ['A'] = 'R',
            ['B'] = 'P',
            ['C'] = 'Q'
        };
        
        var win = new Dictionary<char,char>
        {
            ['A'] = 'Q',
            ['B'] = 'R',
            ['C'] = 'P'
        };

        return movement switch
        {
            'Y' => draw[enemyMovement],
            'X' => lose[enemyMovement],
            'Z' => win[enemyMovement],
            _ => throw new ArgumentOutOfRangeException(nameof(movement), movement, null)
        };
    }
}