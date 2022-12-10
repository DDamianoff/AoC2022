namespace Advent22.Day08.Utils;

internal static class Extractor
{
    /***********
    * standard *
    ***********/
    public static Dictionary<(int x, int y), int> ExtractColumn(
        this Dictionary<(int x, int y), int> grid,
        int size,
        int columnNumber) 
        => Enumerable
            .Range(0, size)
            .Select(x => new KeyValuePair<(int x, int y),int>(
                (x, columnNumber),
                grid[(x, columnNumber)]))
            .ToDictionary(
                kvp => kvp.Key, 
                kvp => kvp.Value);
    
    public static Dictionary<(int x, int y), int> ExtractRow(
        this Dictionary<(int x, int y), int> grid,
        int size,
        int rowNumber)
    {
        return Enumerable
            .Range(0, size)
            .Select(y =>
                new KeyValuePair<(int x, int y), int>(
                    (rowNumber, y),
                    grid[(rowNumber, y)]))
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value);
    }


    /**********
    * reverse *
    **********/ 
    public static Dictionary<(int x, int y), int> ExtractReversedColumn(
        this Dictionary<(int x, int y), int> grid,
        int size,
        int columnNumber) =>
        Enumerable
            .Range(0, size).Reverse()
            .Select(x => new KeyValuePair<(int x, int y), int>(
                (x, columnNumber),
                grid[(x, columnNumber)]))
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value);

    public static  Dictionary<(int x, int y), int> ExtractReversedRow(
        this Dictionary<(int x, int y), int> grid,
        int size,
        int rowNumber)
    {
        return Enumerable
            .Range(0, size).Reverse()
            .Select(y =>
                new KeyValuePair<(int x, int y), int>(
                    (rowNumber, y),
                    grid[(rowNumber, y)]))
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value);
    }

    /**********
    * partial *
    **********/
    public static Dictionary<(int x, int y), int> ExtractColumn(
        this Dictionary<(int x, int y), int> grid, 
        int columnNumber,
        int size,
        int startingIndex)
    {
        columnNumber--;
        return Enumerable
            .Range(0, size)
            .Select(x => new KeyValuePair<(int x, int y), int>(
                (x, columnNumber),
                grid[(x, columnNumber)]))
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value);
    }

    public static Dictionary<(int x, int y), int> ExtractRow(
        this Dictionary<(int x, int y), int> grid, 
        int rowNumber,
        int size,
        int startingIndex) 
        => Enumerable
            .Range(0, size)
            .Select(y => 
                new KeyValuePair<(int x, int y), int>(
                    (rowNumber, y), 
                    grid[(rowNumber, y)]))
            .ToDictionary(
                kvp => kvp.Key, 
                kvp => kvp.Value);
}