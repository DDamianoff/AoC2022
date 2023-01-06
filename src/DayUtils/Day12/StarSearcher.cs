using Advent22.Utils.Cardinals;

namespace Advent22.DayUtils.Day12;

public class StarSearcher
{
    private readonly Dictionary<Coordinate, PixelStarNode> _grid;
    private readonly PixelStarNode _start;
    private readonly PixelStarNode _goal;

    private bool _previouslyPerformedSuccessfulSearch;
    
    private PixelStarNode Current
    { get; set; }

    public StarSearcher(Dictionary<Coordinate, Pixel> grid, Coordinate  start, Coordinate goal)
    {
        _grid = grid.ToDictionary(
            n => n.Key,
            n => new PixelStarNode(n.Value));
        _start = _grid[start];
        
        _goal = _grid[goal];
        
        _start.Cost = DistanceBetween(_start, _goal);
        _start.TargetDistance = HeuristicDistanceBetween(_start, _goal);
        
        _goal.Cost = int.MaxValue;
        _goal.TargetDistance = HeuristicDistanceBetween(_goal, _goal);
        
        Current = _start;
    }

    public bool PerformSearch()
    {
        var openList = new PriorityQueue<PixelStarNode, double>();

        openList.Enqueue(Current, Current.TotalCost);
        
        while (openList.Count != 0)
        {
            Current = openList.Dequeue();
            
            if (Current.PreviouslyExpanded)
                continue;

            var adjacent = GetAdjacent(Current);
            
            foreach (var adjacentNode in adjacent)
            {
                if (!Current.CanMoveTo(adjacentNode))
                    continue;
                
                var possiblyLowerCost = Current.Cost + DistanceBetween(Current, adjacentNode);

                if (possiblyLowerCost >= adjacentNode.Cost) 
                    continue;
                
                adjacentNode.Parent = Current;
                adjacentNode.Cost = possiblyLowerCost;

                adjacentNode.TargetDistance = HeuristicDistanceBetween(adjacentNode, _goal);
                
                if (!adjacentNode.PreviouslyExpanded)
                    openList.Enqueue(adjacentNode, adjacentNode.TotalCost);
            }
            
            Current.PreviouslyExpanded = true;
        }
        
        
        _previouslyPerformedSuccessfulSearch = _goal.Parent is not null;

        return _previouslyPerformedSuccessfulSearch;
    }

    private static double DistanceBetween(PixelStarNode a, PixelStarNode b)
    {
        return Math.Sqrt(Math.Pow(a.Location.X - b.Location.X, 2) + 
                         Math.Pow(a.Location.Y - b.Location.Y, 2) );
    }
    
    private static double HeuristicDistanceBetween(PixelStarNode a, PixelStarNode b)
    {
        var distance =  DistanceBetween(a,b);
        
        var deepBonus = 0;

        if (b.Deep == a.Deep + 1)
            deepBonus = -2;
        
        if (a.Deep < b.Deep)
            deepBonus = -1;
                
        if (a.Deep > b.Deep)
            deepBonus = Math.Abs(a.Deep - b.Deep);

        return distance + deepBonus;
    }

    public Stack<PixelStarNode> BuildPath()
    {
        if (!_previouslyPerformedSuccessfulSearch)
            throw new InvalidOperationException("Path not found. Did you search in first place?");
        
        var path = new Stack<PixelStarNode>();
        
        var internalCurrent = _goal;

        do
        {
            if (internalCurrent!.Parent is not null)
                internalCurrent.Direction = internalCurrent.Location.EstimatedDirectionTo(internalCurrent.Parent.Location);
            
            path.Push(internalCurrent);
            
            if (internalCurrent is null)
                throw new Exception("wtf");
            
            internalCurrent = internalCurrent.Parent;
            
        } while (internalCurrent != _start);
        
        return path;
    }

    private IEnumerable<PixelStarNode> GetAdjacent(PixelStarNode node)
    {
        var resultList = new List<PixelStarNode>();

        var adjacentDirectionCross = node.Location.AdjacentCross();

        foreach (var coordinate in adjacentDirectionCross)
        {
            var exists = _grid.TryGetValue(coordinate, out var adjacentNode);
            
            if (exists)
                resultList.Add(adjacentNode);
        }
        
        return resultList;
    }
}