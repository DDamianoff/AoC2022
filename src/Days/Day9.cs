using Advent22.Day09.Utils;

namespace Advent22;

public static class Day9
{
    public static void DayNine()
    {
        var input = File.ReadAllLines("./inputs/D0X.txt");
        var sequenceList = DirectionInterpreter.GetMultipleFromStringCommand(input);
        
        HashSet<(int x, int y)> tailUsedPositions = new();
        
        var head = (0, 0);
        var tail = (0, 0);
        
        foreach (var direction in sequenceList)
        {
            head.MoveTo(direction.RelativeCoords);
            
            if (tail.IsNearBy(head))
                continue;

            var tailTargetCoordinates = GetTailTargetCoordinates(direction.Direction, head);
            
            tail.MoveTo(tailTargetCoordinates);
            
            tailUsedPositions.Add(tailTargetCoordinates);
        }
        tailUsedPositions
            .Count
            .Display("total tail used positions");
    }

    private static void MoveTo(ref this (int x, int y) head, (int x, int y) direction) 
        => head = head.Operate(direction);
    
    private static bool IsNearBy(this (int x, int y) particleA, (int x, int y) particleB)
    {
        return particleA
            .GetAdjacentCoords()
            .Values
            .Any(c => c == particleB);
    }

    private static (int x, int y) GetTailTargetCoordinates(CardinalPoint direction, (int x, int y) headCoords)
    {
        var relativeDirectionCoordinates = DirectionInterpreter.GetSingle(direction.Opposite(), 1);
        return headCoords.Operate(relativeDirectionCoordinates.RelativeCoords);
    }
}