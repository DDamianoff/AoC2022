#nullable enable
namespace Advent22.Day07.Utils;

internal interface INode
{
    DirectoryNode? Parent 
    { get; }
    
    string Name 
    { get; }

    int GetSize();
}