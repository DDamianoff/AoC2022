using Advent22.Day08.Utils;

namespace Advent22;

public static class Day8
{
    public static void DayEight()
    {
        var input = File.ReadAllLines("./inputs/D08.txt");

        var grid = new SquareGrid(input);

        var trees = grid.ImTiredNeedContinueWithMyLife().Count;
        
        trees.Display("total hidden trees");
    }
}