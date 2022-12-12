#nullable enable
namespace Advent22.DayUtils.Day07;

internal interface INode
{
    DirectoryNode? Parent 
    { get; }
    
    string Name 
    { get; }

    int GetSize();
}