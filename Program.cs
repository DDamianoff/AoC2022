var rawValueList = File.ReadAllText("./day_one.txt");

var parsedElves = rawValueList
    .Split("\n\n")
    .Select(e => 
        e.Split("\n"))
    .Select(elf => 
        elf
            .Select(int.Parse)
            .Sum())
    .ToList();

parsedElves
    .Max()
    .Display("Elf with most calories");

parsedElves
    .Order()
    .TakeLast(3)
    .Sum()
    .Display("Sum of top three elves with most calories");

internal static class Utils
{
    public static void Display(this object finalValue, string title) 
        => Console.WriteLine($"{title}: {finalValue}");
}