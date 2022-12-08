#nullable enable
namespace Advent22.Day07.Utils;

internal sealed class FileNode : INode
{
    public FileNode(DirectoryNode parent, string name, int size)
    {
        Parent = parent;
        Name = name;
        Size = size;
    }

    public DirectoryNode Parent { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public int GetSize() => Size;
}