namespace Advent22.DayUtils.Day11;

public readonly struct ApeTest
{
    public ApeTest(int targetApe1, int targetApe2, int divisionNumber)
    {
        TargetApe1 = targetApe1;
        TargetApe2 = targetApe2;
        DivisionNumber = divisionNumber;
    }

    private int DivisionNumber
    { get; }
    public int TargetApe1    
    { get; }
    
    public int TargetApe2
    { get; }
    
    /// <summary>
    /// returns Id of target ape to throw the object
    /// </summary>
    public int Test(int item) 
        => item % DivisionNumber == 0 
            ? TargetApe1 
            : TargetApe2;
    }