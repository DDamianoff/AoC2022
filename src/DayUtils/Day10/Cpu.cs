namespace Advent22.DayUtils.Day10;

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

