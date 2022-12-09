// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
namespace Advent22.Day08.Utils;

internal partial class Grid
{
    private class Sweeper
    {
        private readonly int _size;
        private readonly Func<bool> _condition;
        private readonly int[,] _grid;
        public Sweeper(ref int[,] grid)
        {
            _grid = grid;
            
            _size = grid.GetLength(0);
            _condition = (x,y) =>
            {
                _grid
            };
        }

        

        internal List<(int x, int y)> SweepRight()
        {
            var result = new List<(int x, int y)>();
            for (var x = 0; x < _size; x++)
            for (var y = 0; y < _size; y++)
                if (_condition(x,y))
                    result.Add((x,y));
            return result;
        }

        internal List<(int x, int y)> SweepLeft()
        {
            var result = new List<(int x, int y)>();
            for (var x = _size - 1; x >= 0; x--)
            for (var y = 0; y < _size; y++)
                if (_condition())
                    result.Add((x,y));
            return result;
        }

        internal List<(int x, int y)> SweepTop()
        {
            var result = new List<(int x, int y)>();
            for (var y = 0; y < _size; y++)
            for (var x = 0; x < _size; x++)
                if (_condition())
                    result.Add((x,y));
            return result;
        }

        internal List<(int x, int y)> SweepBottom()
        {
            var result = new List<(int x, int y)>();
            for (var y = _size - 1; y >= 0; y--)
            for (var x = 0; x < _size; x++)
                if (_condition())
                    result.Add((x,y));
            return result;
        }
        internal IEnumerable<(int x, int y)> SweepAll()
        {
            var result = new List<(int x, int y)>();
            
            result.AddRange(SweepRight());
            result.AddRange(SweepLeft());
            result.AddRange(SweepTop());
            result.AddRange(SweepBottom());

            return result.Distinct();
        }
    }
    
    void FindVisibleTrees()
    {
        var sweeper = new Sweeper(ref _grid);
        
        
    }
}