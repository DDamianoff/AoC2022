// ReSharper disable MemberCanBePrivate.Local

using System.Collections.Immutable;

namespace Advent22.DayUtils.Day08;

internal partial class SquareGrid
{
    public HashSet<(int x, int y)> SearchVisibleTrees()
    {
        var visibleTreeCoords = new HashSet<(int x, int y)>();
        
        for (int i = 0; i < _size; i++)
        {
            var directionGroup = new List<Dictionary<(int x, int y),int>>
            {
                _gridV2.ExtractRow(
                    size: _size, 
                    rowNumber: i),
                _gridV2.ExtractReversedRow(
                    size: _size, 
                    rowNumber: i),
                _gridV2.ExtractColumn(
                    size: _size, 
                    columnNumber: i),
                _gridV2.ExtractReversedColumn(
                    size: _size, 
                    columnNumber: i),
            };
            
            foreach (var treeGroup in directionGroup)
            {
                var max = -1;
                foreach (var tree in treeGroup.Where(tree => tree.Value > max))
                {
                    visibleTreeCoords.Add(tree.Key);
                    max = tree.Value;
                    
                    if (tree.Value == 9)
                        break;
                }
            }
        }
        return visibleTreeCoords;
    }
    public int GetTopScenicValue()
    {
        var usefulValues = _gridV2
            .Where(kvp => kvp.Value >= 5)
            .Select(k => k)
            .ToList();
        var max = -1;
        
        foreach (var currentTree in usefulValues)
        {
            #region column-row

            var col = _gridV2
                .Where(k => k.Key.col == currentTree.Key.col)
                .OrderBy(k => k.Key.col)
                .ToImmutableList();
            
            var row = _gridV2
                .Where(k => 
                    k.Key.row == currentTree.Key.row)
                .OrderBy(k => k.Key.row)
                .ToImmutableList();

            #endregion

            #region cross

            var left = row.First();
            foreach (var current in row.Take(row.IndexOf(currentTree)).Reverse())
            {
                if (current.Value < currentTree.Value) 
                    continue;
                left = current;
                break;
            }

            var right = row.Last();
            foreach (var current in row.Skip(row.IndexOf(currentTree) + 1))
            {
                if (current.Value < currentTree.Value) 
                    continue;
                right = current;
                break;
            }
           
            var top = col.First();
            foreach (var current in col.Take(col.IndexOf(currentTree)).Reverse())
            {
                if (current.Value < currentTree.Value) 
                    continue;
                top = current;
                break;
            }

            var bottom = col.Last();
            foreach (var current in col.Skip(col.IndexOf(currentTree) + 1))
            {
                if (current.Value < currentTree.Value) 
                    continue;
                bottom = current;
                break;
            }
            
            #endregion

            #region Cross Scenic Values

            var leftScenicScore = CalculateCardinalDistance(
                a: currentTree.Key.col, 
                b: left.Key.col);

            var rightScenicScore = CalculateCardinalDistance(
                a: currentTree.Key.col, 
                b: right.Key.col);
            
            var bottomScenicScore = CalculateCardinalDistance(
                a:bottom.Key.row,
                b:currentTree.Key.row);
            
            var topScenicScore = CalculateCardinalDistance(
                a:top.Key.row,
                b:currentTree.Key.row);

            #endregion

            var score = leftScenicScore * rightScenicScore * topScenicScore * bottomScenicScore;
            
            max = score > max 
                ? score
                : max;
        }

        return max;
    }

    private static int CalculateCardinalDistance(int a, int b) => Math.Abs(a - b);
}
    /* Dunno why this doesn't work:
var left = row
    .Take(row.IndexOf(currentTree))
    .Reverse()
    .FirstOrDefault(x => 
        x.Value >= currentTree.Value, 
        row.First());

var right = row
    .Skip(row.IndexOf(currentTree) + 1)
    .FirstOrDefault(k => 
        k.Key.row >= currentTree.Key.row, 
        row.Last());

var top = col
    .Take(col.IndexOf(currentTree))
    .Reverse()
    .FirstOrDefault(k => 
        k.Key.col >= currentTree.Key.col, 
        col.First());

var bottom = col
    .Skip(col.IndexOf(currentTree) + 1)
    .FirstOrDefault(k => 
        k.Key.col >= currentTree.Key.col, 
        col.Last());
*/