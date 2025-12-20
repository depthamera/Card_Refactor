using Unity.Mathematics;

namespace CardRefactor.Core.Domain
{
	public class BoardModel
	{
        private readonly BlockData[,] _grid;

        public int Width { get; }
        public int Height { get; }

        public BoardModel(int width, int height)
        {
            if(width <= 0 || height <= 0)
            {
                throw new System.ArgumentOutOfRangeException
                (
                    $"Width and Height must be positive. Invalid size: {width}x{height}"
                );
            }

            Width = width;
            Height = height;
            _grid = new BlockData[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _grid[x, y] = new BlockData(BlockType.None);
                }
            }
        }

        public BoardModel(int2 size) : this(size.x, size.y) { }

        public BlockData GetBlock(int x, int y)
        {
            if(!IsValid(x, y))
            {
                return null;
            }

            return _grid[x, y];
        }

        public BlockData GetBlock(int2 position)
        {
            return GetBlock(position.x, position.y);
        }

        public bool IsValid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public bool IsValid(int2 position)
        {
            return IsValid(position.x, position.y);
        }

        public void SetBlock(int x, int y, BlockData newBlockData)
        {
            if(!IsValid(x, y))
            {
                return;
            }

            if(newBlockData == null)
            {
                return;
            }

            _grid[x, y] = newBlockData;
        }

        public void SetBlock(int2 position, BlockData newBlockData)
        {
            SetBlock(position.x, position.y, newBlockData);
        }
    }
}