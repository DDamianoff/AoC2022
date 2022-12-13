namespace Advent22.DayUtils.Day11;

public partial class Ape
{
    public int MonkeyId
    { get; }
    
    public Queue<int> HoldingItems
    { get; set; }

    public List<Ape> ApeGroup
    { get; }
    
    private IAperation Operation
    { get; }
    
    private int TestValue
    { get; }

    private ApeTest Test
    { get; }

    public int InspectedTimes
    {
        get;
        private set;
    }

    public void PlayMonkeyTurn()
    {
        
        if (HoldingItems.Count == 0)
            return;
        
        HoldingItems.Count.Times(ModifyItem);
        
        HoldingItems.Count.Times(ThrowToAnotherApe);
    }

    private void ThrowToAnotherApe()
    {
        var item = HoldingItems.Dequeue();
        var targetApe = Test.Test(item);
        ApeGroup[targetApe].HoldingItems.Enqueue(item);
    }

    private void ModifyItem()
    {
        InspectedTimes++;
        var modified = Operation.Operate(HoldingItems.Dequeue());
        HoldingItems.Enqueue(modified);
    }

    public override string ToString()
        => $"Ape {MonkeyId}".PadRight(10) +
           $"threw {InspectedTimes}".PadRight(10) +
           "times";
}