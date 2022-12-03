namespace Advent22;

internal static class Utils
{
    public static void Display(this object finalValue, string title) 
        => Console.WriteLine($"{title}: {finalValue}");

    public static void Times(this int times, Action acton) 
    {
        for (int i = 0; i < times; i++) 
            acton.Invoke();
    }
}

