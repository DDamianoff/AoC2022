using Advent22;

namespace Advent22;

/// <summary>
/// The idea is:
/// Since the two parts of the problem makes
/// 'X,Y,Z' differ in meaning ("rock, paper, scissors" vs "win, draw, lose")
/// This solution parses those values to 'rock, paper, scissors' (P,Q,R).
/// For the problem one: just using a dictionary (X => P).
/// For the problem two: determining werther we want to lose, win or draw (dictionaries)
/// Each of those receives the enemy's move as a key and return your move.
///
/// Both cases returns a string like "AQ", witch would be used
/// for another function to compute the round's value.
/// 
/// </summary>
public static class Day2
{
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
    
    internal static int GetBonus(this string match) => match[1] switch
    {
        'P' => 1,
        'Q' => 2,
        'R' => 3,
        _ => throw new Exception("Shouldn't happen but makes Roslyn happy")
    };
    
    internal static int GetPoints(this string match) =>
        match switch
        {
            "AQ" or "BR" or "CP" => 6,
            "AP" or "BQ" or "CR" => 3,
            "AR" or "BP" or "CQ" => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(match), match, null)
        };

    internal static char OperateStrategy(this char movement) => 
        movement switch
        {
            'X' => 'P',
            'Y' => 'Q',
            'Z' => 'R',
            _ => throw new ArgumentOutOfRangeException(nameof(movement), movement, null)
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