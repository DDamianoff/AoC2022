// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
// ReSharper disable MemberCanBePrivate.Local
namespace Advent22.Day08.Utils;

internal partial class SquareGrid
{
    public HashSet<(int x, int y)> ImTiredNeedContinueWithMyLife()
    {
        var visibleTreeCoords = new HashSet<(int x, int y)>();
        
        // right => left
        for (var x = 0; x < _size; x++)
        {
            var highestTree = -1;
            
            for (var y = 0; y < _size; y++)
            {
                var coords = (x, y);
                var currentTree = _grid[coords.x, coords.y];
                
                if (currentTree > highestTree)
                {
                    highestTree = currentTree;
                    visibleTreeCoords.Add(coords);
                }
                
                if (currentTree == 9 || 
                    highestTree == 9)
                    break;
            }
        }
        
        // left => right
        for (var x = 0; x < _size; x++)
        {
            var highestTree = -1;
            
            for (var y = _size - 1; y >= 0; y--)
            {
                var coords = (x, y);
                var currentTree = _grid[coords.x, coords.y];
                
                if (currentTree > highestTree)
                {
                    highestTree = currentTree;
                    visibleTreeCoords.Add(coords);
                }
                
                if (currentTree == 9 || 
                    highestTree == 9)
                    break;
            }
        }
        
        // top => bottom
        for (var y = 0; y < _size; y++)
        {
            var highestTree = -1;
            for (var x = 0; x < _size; x++)
            {
                var coords = (x, y);
                var currentTree = _grid[coords.x, coords.y];
                
                if (currentTree > highestTree)
                {
                    highestTree = currentTree;
                    visibleTreeCoords.Add(coords);
                }
                
                if (currentTree == 9 || 
                    highestTree == 9)
                    break;
            }
        }
        
        // bottom => top
        for (var y = 0; y < _size; y++)
        {
            var highestTree = -1;
            for (var x = _size - 1; x >= 0; x--)
            {
                var coords = (x, y);
                var currentTree = _grid[coords.x, coords.y];
                
                if (currentTree > highestTree)
                {
                    highestTree = currentTree;
                    visibleTreeCoords.Add(coords);
                }
                
                if (currentTree == 9 || 
                    highestTree == 9)
                    break;
            }
        }
        
        return visibleTreeCoords;
    }
}