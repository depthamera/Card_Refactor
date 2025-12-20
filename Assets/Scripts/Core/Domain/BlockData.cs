namespace CardRefactor.Core.Domain
{
	public struct BlockData : System.IEquatable<BlockData>
	{
        public BlockType Type { get; private set; }

        public BlockData(BlockType type)
        {
            Type = type;
        }

        public readonly bool Equals(BlockData other)
        {
            return Type == other.Type;
        }

        public override readonly bool Equals(object obj)
        {
            return obj is BlockData other && Equals(other);
        }

        public override readonly int GetHashCode()
        {
            return Type.GetHashCode();
        }
    }
}