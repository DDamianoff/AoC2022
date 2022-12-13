namespace Advent22.DayUtils.Day11;

public readonly struct ApeMultiplies : IAperation
{
    public ApeMultiplies(int value) => Value = value;
    

    public int Value { get; }
    public int Operate(int oldValue)
    {
        if (Value == -1)
            return (oldValue * oldValue) / 3;
        
        return (Value * oldValue) / 3;
    }
}