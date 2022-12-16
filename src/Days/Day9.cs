using Advent22.DayUtils.DaY09;
using Advent22.Utils;
using Advent22.Utils.Cardinals;

namespace Advent22;

public static class Day9
{
    public static void DayNinePartOne()
    {
        var input = File.ReadAllLines("./inputs/D09.txt");
        var sequenceList = DirectionInterpreter.GetMultipleFromStringCommand(input);
        
        var tailUsedPositions = new HashSet<Coordinate>();
        
        var head = new Coordinate(0, 0);
        var tail = new Coordinate(0, 0);

        tailUsedPositions.Add(tail); // add initial position
        
        foreach (var direction in sequenceList)
        {
            for (var _ = 0; _ < direction.Count; _++)
            {
                var headTargetCoordinates = head.Apply(direction.RelativeCoords);
                
                head.MoveTo(headTargetCoordinates);

                if (tail.IsNearBy(head)) 
                    continue;
                
                var tailTargetCoordinates = GetNextToTarget(tail, head);

                tail.MoveTo(tailTargetCoordinates);

                tailUsedPositions.Add(tail);
            }
        }
        tailUsedPositions
            .Count
            .Display("total tail used positions");
    }

    public static void DayNinePartTwo()
    {
        var input = File.ReadAllLines("./inputs/D0X.txt");
        var sequenceList = DirectionInterpreter.GetMultipleFromStringCommand(input);
        
        var tailUsedPositions = new HashSet<Coordinate>();

        var rope = Enumerable
            .Range(0, 10)
            .Select(_ => new Coordinate(0,0))
            .ToArray(); // yes, I'm too lazy to properly fill an array 
        
        ref var head = ref rope[0];
        ref var tail = ref rope[^1];

        tailUsedPositions.Add(tail); // add initial position
        
        foreach (var direction in sequenceList)
        {
            for (var _ = 0; _ < direction.Count; _++)
            {
                var headTargetCoordinates = head.Apply(direction.RelativeCoords);
                
                var trace = head.MoveTo(headTargetCoordinates);

                for (var i = 1; i < rope.Length; i++)
                {
                    ref var current = ref rope[i];
                    ref var next = ref rope[i+1];
                    
                    if (current.IsNearBy(next))
                        break;
                    
                    var currentTargetCoordinates = (0,0);
                    
                    try
                    {
                        currentTargetCoordinates = GetNextToTarget(current, next);
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine($"i: {i}"
                                          + Environment.NewLine +
                                          $"_: {_}" 
                                          + Environment.NewLine +
                                          $"direction: {direction}" 
                                          + Environment.NewLine +
                                          $"Current: {current}" 
                                          + Environment.NewLine +
                                          $"Next: {next}" 
                                          + Environment.NewLine +
                                          $"state of list: " 
                                          + Environment.NewLine +
                                          $"{rope
                                              .Select(a =>$"p = ({a.X});({a.Y})")
                                              .ToList()
                                              .Aggregate( (crt, nxt) => 
                                              string.Concat(crt, nxt, Environment.NewLine))}");
                        return;
                    }

                    trace = current.MoveTo(trace);
                    
                    // if tail:
                    if (i == rope.Length - 1)
                        tailUsedPositions.Add(tail);
                }
            }
        }
        tailUsedPositions
            .Count
            .Display("same but for the rope-snake");
    }
    
    private static Coordinate MoveTo(ref this Coordinate target, Coordinate newCoordinates)
    {
        var trace = target;
        target = newCoordinates;
        return trace;
    }

    private static bool IsNearBy(this Coordinate particleA, Coordinate particleB)
    {
        var adjacentCoords = particleA
            .GetAdjacentCoords()
            .Values;
        
        var result = adjacentCoords.Any(c => c == particleB);
        return result;
    }


    private static Coordinate GetNextToTarget(this Coordinate currentKnot, Coordinate nextKnot)
    {
        // *
        var validJumpingNodes = nextKnot.GetCrossAdjacentCoords();
        var possibilities = currentKnot.GetAdjacentCoords();

        return possibilities
            .Values
            .Intersect(validJumpingNodes.Values)
            .First();
    }
}

/* los movimientos diagonales no son legales. es necesario mover cardinalmente.
         * si comparas dos cuadrados de adyacentes, son posibles 2/3 coincidencias:
         *   ++*··    + = adyacentes de 'a'
         *   +a*b·    · = adyacentes de 'b'
         *   ++*··    * = coincidentes
         *
         *          +a+
         *   +a*··  +**·
         *   ++*b·   ·b·
         *  En cualquier caso, no hay un resultado contundente.
         *  Comparar vs una cruz, en cambio, siempre deja solamente un único posible resultado
         *
         *    +  ···     +···
         *   +a+ ·b· =  +a*b·
         *    +  ···     +···
         *
         *   +a*··   +a+
         *   ++·b·    *··
         *            ·b·
         */