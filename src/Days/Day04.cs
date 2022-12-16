using Advent22.Utils;

namespace Advent22;

public static class Day4
{
    public static void DayFour()
    {
        var input = File.ReadAllLines("./inputs/D04.txt");
        var elfPair = input
                .Select(e => e.Split(","))
                .Select(e => (e[0],e[1]))           // ("1-1", "1-1") 
                .Select(ep => (
                        ep.Item1.Split("-")
                            .Select(int.Parse)
                            .ToArray() ,
                        ep.Item2.Split("-")
                            .Select(int.Parse)
                            .ToArray()))                  // ([1,1] [2,1]) 
                .Select(p => 
                    new CleaningOverlap(
                        elfOne: (
                            start: p.Item1[0],
                            end: p.Item1[1]),
                        elfTwo: (
                            start: p.Item2[0],
                            end: p.Item2[1])))
                .ToList();
        
        elfPair
            .Count(p => p.AnyContained())
            .Display("Count of total full overlaps");

        elfPair
            .Count(p => p.AnyOverlap())
            .Display("Count of overlaps");
        
        /* Visualize */
        /*
         * elfPair.ForEach(_ => 
         *   _.GetFullEvaluation()
         *       .Display(_.ToString()));
         */
    }

    
    // THIS IS DEFINITELY NOT MAINTAINABLE, GOOD
    // neither something I'd do in a serious project.
    // But it's crazy and fun to see how much I can push C# syntax
    internal static bool IsInsideOf(this (int min,int max) firstPair, (int min,int max) secondPair) 
        => firstPair.min.InRange(secondPair) && firstPair.max.InRange(secondPair);
    
    internal static bool Overlaps(this (int min,int max) firstPair, (int min,int max) secondPair) 
        => firstPair.min.InRange(secondPair) || firstPair.max.InRange(secondPair);
}

internal class CleaningOverlap
{
    // I Know that I SHOULD declare an 'Elf' Type class like:
    // Elf {startVal, endVal}
    // BUT, I want to try and practice with tuples.
    internal CleaningOverlap((int start, int end) elfOne, (int start, int end) elfTwo)
    {
        ElfOne = elfOne;
        ElfTwo = elfTwo;
    }

    internal (int min, int max) ElfOne 
    { get; set; }
    
    internal (int min, int max) ElfTwo 
    { get; set; }
    
    internal bool AnyContained() 
        => ElfOne.IsInsideOf(ElfTwo) 
           || 
           ElfTwo.IsInsideOf(ElfOne);
    
    internal bool AnyOverlap() 
        => ElfOne.Overlaps(ElfTwo) 
           || 
           ElfTwo.Overlaps(ElfOne);

    public string GetFullEvaluation()
    {
        var detail = this.ToString();
        
        detail += $"ElfOne is FULLY contained in ElfTwo: {ElfOne.IsInsideOf(ElfTwo)}"
                  + Environment.NewLine;
        detail += $"ElfTwo is FULLY contained in ElfOne: {ElfTwo.IsInsideOf(ElfOne)}"
                  + Environment.NewLine;
        
        detail += $"ElfOne overlaps ElfTwo: {ElfOne.IsInsideOf(ElfTwo)}"
                  + Environment.NewLine;
        detail += $"ElfTwo overlaps ElfOne: {ElfTwo.IsInsideOf(ElfOne)}"
                  + Environment.NewLine;
        
        detail += $"Any contained: {AnyContained()}" 
                  + Environment.NewLine;
        detail += $"Any overlap: {AnyOverlap()}" 
                  + Environment.NewLine
                  + Environment.NewLine;
        
        return detail;
    }
    
    public string GetEvaluation()
    {
        var detail = this.ToString();
        
        detail += $"Any contained: {AnyContained()}" 
                       + Environment.NewLine;
        detail += $"Any overlapped: {AnyOverlap()}" 
                       + Environment.NewLine
                       + Environment.NewLine;
        return detail;
    }

    public override string ToString()
    {
        var detail = string.Empty;
        
        detail += $"[ {ElfOne.min} - {ElfOne.max} ]";
        detail += " , ";
        detail += $"[ {ElfTwo.min} - {ElfTwo.max} ]";
        detail += Environment.NewLine;
        
        return detail;
    }
}