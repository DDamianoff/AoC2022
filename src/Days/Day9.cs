using Advent22.Day09.Utils;

namespace Advent22;

public static class Day9
{
    public static void DayNine()
    {
        var input = File.ReadAllLines("./inputs/D0X.txt");
        var sequenceList = DirectionInterpreter.GetMultipleFromStringCommand(input);
        
        var tailUsedPositions = new HashSet<(int x, int y)>();
        
        var head = (0, 0);
        var tail = (0, 0);

        tailUsedPositions.Add(tail); // add initial position
        
        foreach (var direction in sequenceList)
        {
            foreach (var _ in Enumerable.Range(1, direction.Count))
            {
                var headTargetCoordinates = head.Operate(direction.RelativeCoords);
                
                head.MoveTo(headTargetCoordinates);

                if (tail.IsNearBy(head)) 
                    continue;
                
                var tailTargetCoordinates = GetTailTargetCoordinates(direction.Direction, head);

                tail.MoveTo(tailTargetCoordinates);

                tailUsedPositions.Add(tail);
            }
        }
        tailUsedPositions
            .Count
            .Display("total tail used positions");
    }

    // ReSharper disable once RedundantAssignment
    private static void MoveTo(ref this (int x, int y) target, (int x, int y) newCoordinates) 
        => target = newCoordinates;
    
    private static bool IsNearBy(this (int x, int y) particleA, (int x, int y) particleB)
    {
        var adjacentCoords = particleA
            .GetAdjacentCoords()
            .Values;
        
        var result = adjacentCoords.Any(c => c == particleB);
        return result;
    }

    private static (int x, int y) GetTailTargetCoordinates(CardinalPoint direction, (int x, int y) headCoords)
    {
        var relativeDirectionCoordinates = DirectionInterpreter.GetSingle(direction.Opposite());
        return headCoords.Operate(relativeDirectionCoordinates.RelativeCoords);
    }
}

/* Expected outputs foreach stage
== R 4 ==
  
H(4,0)
T(3,0)

== U 4 ==
  
H(4,4)
T(4,3)

== L 3 ==
  
H(1,4)
T(2,4)

== D 1 ==
  
H(1,3)
T(2,4)

== R 4 ==
  
H(5,3)
T(4,3)

== D 1 ==
  
H(5,2)
T(4,3)

== L 5 ==
  
H(0,2)
T(1,2)

== R 2 ==
  
H(2,2)
T(1,2)
*/