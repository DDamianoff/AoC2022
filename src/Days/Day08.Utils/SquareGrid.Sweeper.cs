// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
// ReSharper disable MemberCanBePrivate.Local
namespace Advent22.Day08.Utils;

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
}