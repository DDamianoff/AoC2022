#nullable enable
namespace Advent22.Day07.Utils;
internal class DirectoryNode : INode
{
    public DirectoryNode(DirectoryNode? parent, string name)
    {
        Parent = parent;
        Name = name;
    }

    public DirectoryNode? Parent { get; set; }
    
    public string Name { get; set; }

    public bool PreviouslyProcessed = false;

    public readonly List<INode> Children = new ();

    public List<DirectoryNode> DirectoryChildren => Children.OfType<DirectoryNode>().ToList();

    public int GetSize() 
        => Children.Sum(c => c.GetSize());

    public int Size => GetSize();

    public static DirectoryNode GenerateInitialNode() => new (null, "/");

    public List<DirectoryNode> GetTinyDirectories()
    {
        var onCurrent = DirectoryChildren
            .Where(d => 
                d.GetSize() < 100000)
            .ToList();

        foreach (var childMajor in DirectoryChildren.Select(d => d.GetTinyDirectories()))
            onCurrent.AddRange(childMajor);

        return onCurrent
            .Distinct()
            .ToList();
    }
    
    // at this point I'm too lazy, so...
    // I'm just listing all directories and filtering with linq later on.
    public IEnumerable<DirectoryNode> GetAllChildDirectories()
    {
        var onCurrent = DirectoryChildren;

        foreach (var childDir in DirectoryChildren)
            onCurrent.AddRange(childDir.GetAllChildDirectories());

        return onCurrent
            .ToList();
    }
    

    public override string ToString() => $"DirN: {Name} {Size}";
}