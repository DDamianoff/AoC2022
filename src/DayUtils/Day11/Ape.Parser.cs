using System.Text.RegularExpressions;

namespace Advent22.DayUtils.Day11;

public partial class Ape
{
    public Ape(string rawApeData, List<Ape> apes)
    {
        /****
        | 0 | Monkey 0:
        | 1 |      Starting items: 79, 98
        | 2 |      Operation: new = old * 19
        | 3 |      Test: divisible by 23
        | 4 |          If true: throw to monkey 2
        | 5 |          If false: throw to monkey 3
        ****/

        var apeDataAsArray = rawApeData.Split("\n");

        if (apeDataAsArray.Length != 6)
            throw new ArgumentException($"this is NOT an ape: {apeDataAsArray}{Environment.NewLine}", nameof(rawApeData));

        #region [0] Id
        MonkeyId = MatchSingleNumber(apeDataAsArray[0]);
        #endregion
           
        #region [1] Items
        var items = MatchMultipleNumber(apeDataAsArray[1]); 
           
        HoldingItems = new Queue<int>(items);
        #endregion

        #region [2] Operation

        if (apeDataAsArray[2].Contains('*'))
        {
            var opVal = apeDataAsArray[2]
                .Replace(" ", "")
                .Split("*")[1];

            var parsed = int.TryParse(opVal, out var parsedValue)
                ? parsedValue 
                : -1;
            
            Operation = new ApeMultiplies(parsed);
        }
           
        else if (apeDataAsArray[2].Contains('+'))
        {
            var opVal = apeDataAsArray[2]
                .Replace(" ", "")
                .Split("+")[1];

            var parsed = int.Parse(opVal);
            
            Operation = new ApeAdds(parsed);
        }
        else
            throw new ArgumentException($"this is not an aperation: {apeDataAsArray[2]}", nameof(rawApeData), null);
           
        #endregion

        #region [3,4,5] Test

        TestValue = MatchSingleNumber(apeDataAsArray[3]);

        var ape1 = MatchSingleNumber(apeDataAsArray[4]);
        var ape2 = MatchSingleNumber(apeDataAsArray[5]);

        Test = new ApeTest(ape1, ape2,TestValue );

        #endregion

        ApeGroup = apes;
    }

    private static int MatchSingleNumber(string input)
    {
        var match = Regex.Match(input, @"[0-9]{1,6}");
        return int.Parse(match.Value);
    }
    
    private int[] MatchMultipleNumber(string input)
    {
        var matches = Regex.Matches(input, @"[0-9]{1,6}");
        return matches
            .Select(m => 
                int.Parse(m.Value))
            .ToArray();
    }
}