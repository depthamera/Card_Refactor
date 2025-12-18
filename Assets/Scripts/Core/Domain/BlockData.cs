namespace CardRefactor.Core.Domain
{
	public class BlockData
	{
        public BlockType Type { get; private set; }

        public BlockData(BlockType type)
        {
            Type = type;
        }

    }
}