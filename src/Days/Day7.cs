#nullable enable
using Advent22.DayUtils.Day07;

namespace Advent22;

public static class Day7
{
    private const int TotalDiskSpace = 70000000;

    private const int RequiredSpace = 30000000;
    public static void DaySeven()
    {
        var instructionStack = new Stack<string>(File.ReadAllLines("./inputs/D07.txt").Reverse());
        var parent = DirectoryNode.GenerateInitialNode();

        instructionStack.Pop(); // remove the first one: I create
        
        var interpreter = new Interpreter(instructionStack, parent);
        
        interpreter.ApplyAll();
        
        parent
            .GetTinyDirectories()
            .Select(d => d.GetSize())
            .Sum()
            .Display("Sum of directories smaller than 100k");
        
        var required = RequiredSpace  - (TotalDiskSpace - parent.Size);
        
        parent
            .GetAllChildDirectories()
            .Where(d => d.Size >= required)
            .Min(d => d.Size)
            .Display("Size of folder to delete");
    }
}

#nullable disable