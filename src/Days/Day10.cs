
using System.Collections;
using System.Diagnostics.CodeAnalysis;

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

internal partial class Cpu
{
    private int _xRegister = 1;
    private bool _iAmBusy;
    private readonly Queue<string> _rawInstructions;
    private readonly int _expectedInstructions = 245;
    private readonly int[] _valuesIMustReport;
    private List<(int cycle, int signalStrength)> _report = new ();

    public Cpu(Queue<string> rawInstructions, int[] reportValue)
    {
        _rawInstructions = rawInstructions;
        _valuesIMustReport = reportValue;
    }

    internal void StartClock()
    {
        int pendingAddVal = 0;
        for (int cycle = 1; cycle < _expectedInstructions; cycle++)
        {
            if (_valuesIMustReport.Contains(cycle))
            {
                ReportStatus(cycle);
                Console.WriteLine($"Cycle: {cycle.ToString().PadLeft(3, '0')}".PadRight(20,' ') +
                                  $"_xReg:{_xRegister}".PadRight(24, ' ')+
                                  $"report:{_xRegister * cycle}");
            }

            if (_iAmBusy)
            {
                _iAmBusy = false;
                _xRegister += pendingAddVal;
                continue;
            }
            
            if (_rawInstructions.Count == 0)
                break;

            var instruction = Instruction.FromRawInstruction(_rawInstructions.Dequeue());
            
            if (instruction.Type is InstructionType.NOOP 
                or not InstructionType.ADDX)
                continue;

            pendingAddVal = instruction.Count;
            _iAmBusy = true;
        }
    }

    internal int GetReport() => _report.Sum(r => r.signalStrength);

    private void ReportStatus(int cycle)
    {
        _report.Add((cycle, _xRegister*cycle));
    }
}

internal partial class Cpu
{
    private readonly struct Instruction
    {
        internal readonly InstructionType Type;
        internal readonly int Count;

        internal Instruction(int count, InstructionType type)
        {
            Count = count;
            Type = type;
        }
        
        private Instruction(string rawInstruction)
        {
            if (rawInstruction.StartsWith("noop"))
            {
                Count = 0;
                Type = InstructionType.NOOP;
                return;
            }

            if (!rawInstruction.StartsWith("addx"))
                throw new ArgumentException("Tf did you just inserted man. " +
                                            $"instruction: {rawInstruction}" +
                                            $"{Environment.NewLine}", nameof(rawInstruction));
            
            Count = int.Parse(rawInstruction.Split(" ")[1]);
            Type = InstructionType.ADDX;
        }

        internal static Instruction FromRawInstruction(string rawInstruction) => new(rawInstruction);
    }
    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private enum InstructionType 
    {
        NOOP,
        ADDX,
    }
}
