using Advent22.DayUtils.Day10;
using Advent22.Utils;

namespace Advent22;

public static class Day10
{
    public static void DayTen()
    {
        var input = File.ReadAllLines("./inputs/D10.txt");
        var reportValues = new[] {20, 60, 100, 140, 180, 220 };
        var cpu = new Cpu(new Queue<string>(input), reportValues);
        
        cpu.StartClock();
        
        cpu
            .GetReport()
            .Display("status");
    }
}