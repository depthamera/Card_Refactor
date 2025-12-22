using Unity.Mathematics;

namespace CardRefactor.Core.Domain
{
	public sealed class BoardModel
	{
        private readonly BoardData _data;

        public int2 Size => _data.Size;
        public int Width => Size.x;
        public int Height => Size.y;

        public BoardModel(BoardData data)
        {
            _data = data ?? throw new System.ArgumentNullException(nameof(data));
        }

        public BlockData GetBlock(int2 pos)
        {
            return _data.GetBlockRef(pos);
        }

        public void SetBlock(int2 pos, BlockData newBlockData)
        {
            _data.GetBlockRef(pos) = newBlockData;
        }

        public bool IsValidPosition(int2 pos)
        {
            return _data.IsValidPosition(pos);
        }
    }
}