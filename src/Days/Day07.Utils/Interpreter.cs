#nullable enable
using System.Text.RegularExpressions;

namespace Advent22.Day07.Utils;
internal sealed class Interpreter
{
    private readonly Stack<string> _applier;
    private DirectoryNode _current;

    public Interpreter(Stack<string> applier, DirectoryNode initial)
    {
        _applier = applier;
        _current = initial;
    }

    public void ApplyAll()
    {
        while (_applier.Count > 0)
        {
            var currentInstruction = _applier.Pop();
            var insType = DetermineCommandInstruction(currentInstruction);

            Operate(insType, currentInstruction);

        }
    }

    private void Operate(InstructionType insType, string currentInstruction)
    {
        // It may be nice to break this switch into smaller functions
        switch (insType)
        {
            case InstructionType.GoUp:
                _current = _current.Parent 
                           ?? throw  new InvalidOperationException();
                break;
            
            
            case InstructionType.GoDownTo:
                var directory = currentInstruction.Replace("$ cd ", "");
                _current = _current.Children
                    .OfType<DirectoryNode>()
                    .First(c => c.Name == directory);
                break;
            
            
            case InstructionType.ListDirectory:
                if (_current.PreviouslyProcessed)
                    break;
                var toProcess = GetListedItems();

                foreach (var rawData in toProcess)
                {
                    if (rawData.StartsWith("dir"))
                        _current.Children.Add(GenerateDirectory(rawData, _current));
                    else
                        _current.Children.Add(GenerateFile(rawData, _current));
                }
                
                _current.PreviouslyProcessed = true;
                break;

            default:
                throw new InvalidOperationException();
        }
    }

    private FileNode GenerateFile(string fileData, DirectoryNode parent)
    {
        var processedFileData = fileData.Split(" ");
        var size = int.Parse(processedFileData[0]);
        var fileName = processedFileData[1];

        return new FileNode(parent, fileName, size);
    }

    private DirectoryNode GenerateDirectory (string directoryData, DirectoryNode parent)
    {
        var directoryName = directoryData.Split(" ")[1];
        return new DirectoryNode( parent, directoryName);
    }
    
    private IEnumerable<string> GetListedItems()
    {
        var result = new List<string>();
        
        while (true)
        {
            if (_applier.Count == 0)
                break;
            
            if (_applier.Peek().StartsWith("$"))
                break;
            
            result.Add(_applier.Pop());
        }

        result.Reverse();
        return result;
    }
    private InstructionType DetermineCommandInstruction(string instruction)
    {
        if (instruction[0] != '$')
            throw new InvalidOperationException("something that isn't a command made this way to here");
        
        var cdToChildRegex = new Regex("cd [a-zA-Z]*$");
        if (cdToChildRegex.IsMatch(instruction))
            return InstructionType.GoDownTo;
        var eval = instruction[2..];
        return eval switch
        {
            "cd .." => InstructionType.GoUp,
            "ls" => InstructionType.ListDirectory,
            _ => throw new InvalidOperationException("provided instruction wasn't an command instruction")
        };
    }

    public DirectoryNode GetCurrent() => _current;
}