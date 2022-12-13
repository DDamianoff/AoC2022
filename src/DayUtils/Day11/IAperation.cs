namespace Advent22.DayUtils.Day11;

public interface IAperation
{
    int Value 
    { get; }

    int Operate(int oldValue);
}