using System.Diagnostics.CodeAnalysis;

namespace Advent22.DayUtils.Day10;

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