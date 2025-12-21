using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace CardRefactor.Core.Domain
{
    public sealed class BoardData
    {
        private readonly BlockData[] _grid;

        public int2 Size { get; }
        public int Length => _grid.Length;

        public BoardData(int2 size)
        {
            if(math.any(size <= 0))
                throw new System.ArgumentOutOfRangeException(nameof(size), "Board size dimensions must be positive integers.");

            Size = size;
            _grid = new BlockData[size.x * size.y];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetIndex(int2 pos) => pos.x + pos.y * Size.x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref BlockData GetBlockRef(int index)
        {
            if ((uint)index >= (uint)Length)
            {
                ThrowIndexOutOfRange(index);
            }

            return ref _grid[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref BlockData GetBlockRef(int2 pos)
        {
            if(!IsValidPosition(pos))
            {
                ThrowPosOutOfRange(pos);
            }

            return ref _grid[GetIndex(pos)];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValidPosition(int2 pos)
        {
            return (uint)pos.x < (uint)Size.x && (uint)pos.y < (uint)Size.y;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowIndexOutOfRange(int index)
        {
            int maxIndex = Length - 1;
            int x = index % Size.x;
            int y = index / Size.x;
            string message = $"Index {index} is out of range. Valid range is 0..{maxIndex}. Board size: {Size.x}x{Size.y}. Computed position: ({x},{y}).";
            throw new System.ArgumentOutOfRangeException(nameof(index), message);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowPosOutOfRange(int2 pos)
        {
            string message = $"Position {pos} is out of bounds. Valid Size: {Size}.";
            throw new System.ArgumentOutOfRangeException(nameof(pos), message);
        }
    }
}

