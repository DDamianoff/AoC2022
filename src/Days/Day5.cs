using System.Text.RegularExpressions;
using System.Diagnostics;
namespace Advent22;

public static class Day5
{
    private static readonly (string wharehouse, string instrucions) Input;
    
    public static void DayFivePartOne()
    {
        var crateColumns = Input.wharehouse.Split("\n");
        
        var wareHouse = new WareHouse(crateColumns);
        var orderList = Input.instrucions
            .Split("\n")
            .Select(Instructions.FromRawOrderString)
            .ToArray();
        
        wareHouse.ApplyInstruction(orderList);
        
        wareHouse.Display("Part one");
    }
    
    public static void DayFivePartTwo()
    {
        var crateColumns = Input.wharehouse.Split("\n");
        
        var wareHouse = new WareHouse(crateColumns);
        var orderList = Input.instrucions
            .Split("\n")
            .Select(Instructions.FromRawOrderString)
            .ToArray();
        
        wareHouse.ApplyInstructionV2(orderList);
        
        wareHouse.Display("Part two");
    }

    static Day5()
    {
        var rawInput = File.ReadAllText("./inputs/D05.txt").Split("\n\n");
        
        // TODO: this is NOT pretty...
        var parsedWarehouse = rawInput[0]
            .Replace("    ", "[=] ")
            .Replace("    [", "[=] [")
            .Replace("]    ", "] [=]")
            .Replace("]     [", "] [=] [")
            .Replace("      ", " [=]  ")
            .Replace("       ", "  [=]  ")
            .Replace(" ", "");

        parsedWarehouse = Regex
            .Replace(parsedWarehouse, @"\[|\]", "")
            .Rotate()
            .Trim();
        
        Input = (parsedWarehouse, rawInput[1]);
    }
}

internal class WareHouse
{
    internal readonly Dictionary<char,Stack<char>> Storage = new();

    internal WareHouse (IEnumerable<string> abstractWarehouse)
    {
        foreach (var crateColumn in abstractWarehouse)
        {
            var discardedEmpty = crateColumn.Replace("=", "");
            
            Storage[crateColumn[0]] = discardedEmpty.Length == 1
                ? new Stack<char>()
                : new Stack<char>(discardedEmpty[1..].ToCharArray());
        }
    }
    
    internal void ApplyInstructionV2(Instructions[] instructions)
    {
        foreach (var instruction in instructions)
            DoInstructionV2(instruction);
    }
    private void DoInstructionV2(Instructions instructions)
    {
        var origin = Storage[instructions.Origin];
        var destin = Storage[instructions.Target];
        var crates = new Stack<char>();
        instructions.Amount.Times(() => crates.Push(origin.Pop()));
        instructions.Amount.Times(() => destin.Push(crates.Pop()));
    }
    
    internal void ApplyInstruction(Instructions[] instructions)
    {
        foreach (var instruction in instructions)
            ApplyInstruction(instruction);
    }
    internal void ApplyInstruction(Instructions instructions)
    {
        instructions.Amount.Times(() =>
        {
            var origin = Storage[instructions.Origin];
            var destin = Storage[instructions.Target];
            
            var crate = origin.Pop();
            
            destin.Push(crate);
        });
    }

    public override string ToString()
    {
        return Storage.Keys.Aggregate(string.Empty, (current, key) 
            => current + Storage[key].Peek());
    }
}

internal class Instructions
{
    public char Origin
    { get; }
    
    public int Amount
    { get; }
    
    public char Target
    { get; }
    
    
    internal Instructions(string rawOrder)
    {
        rawOrder = rawOrder
            .Replace(" ", "")
            .Replace("move", "");
        var aux = Regex
            .Replace(rawOrder, "[a-z]{2,4}", "|")
            .Split("|");
        
        Amount = int.Parse(aux[0]);
        Origin = aux[1][0];
        Target = aux[2][0];
    }

    public static Instructions FromRawOrderString(string rawOrder) 
        => new(rawOrder);
}