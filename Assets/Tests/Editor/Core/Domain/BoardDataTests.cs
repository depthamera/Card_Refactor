using CardRefactor.Core.Domain;
using NUnit.Framework;
using Unity.Mathematics;

namespace CardRefactor.Tests.Editor.Core.Domain
{
    public sealed class BoardDataTests
    {
        private BoardData _data;
        private readonly int2 _size = new(5, 5);

        [SetUp]
        public void Setup()
        {
            _data = new BoardData(_size);
        }

        [TestCase (0, 1)]
        [TestCase (1, 0)]
        [TestCase (-1, 1)]
        [TestCase (1, -1)]
        [TestCase (-1, -1)]
        public void Constructor_InvalidSize_ThrowsException(int width, int height)
        {
            // Arrange
            var invalidSize = new int2(width, height);

            // Act & Assert
            var ex = Assert.Throws<System.ArgumentOutOfRangeException>(() => new BoardData(invalidSize));
            Assert.That(ex.ParamName, Is.EqualTo("size"));
        }

        #region GetBlockRef

        [TestCase (-1, 1)]
        [TestCase (1, -1)]
        [TestCase (5, 1)]
        [TestCase (1, 5)]
        public void GetBlockRef_InValidPosition_ThrowsException(int x, int y)
        {
            // Arrange
            var newBlock = new BlockData(BlockType.Red);
            var position = new int2(x, y);

            // Act & Assert
            var ex = Assert.Throws<System.ArgumentOutOfRangeException>(() => _data.GetBlockRef(position));
            Assert.That(ex.ParamName, Is.EqualTo("pos"));
        }

        [TestCase (-1)]
        [TestCase (25)]
        public void GetBlockRef_InValidIndex_ThrowsException(int index)
        {
            // Arrange
            var newBlock = new BlockData(BlockType.Red);
            
            // Act & Assert
            var ex = Assert.Throws<System.ArgumentOutOfRangeException>(() => _data.GetBlockRef(index));
            Assert.That(ex.ParamName, Is.EqualTo("index"));
        }

        [Test]
        public void GetBlockRef_ValidPosition_ReturnsCorrectBlockRef()
        {
            // Arrange
            var newBlock = new BlockData(BlockType.Red);
            var position = new int2(2, 3);
            _data.GetBlockRef(position) = newBlock;

            // Act
            ref var retrievedBlock = ref _data.GetBlockRef(position);

            // Assert
            Assert.That(retrievedBlock, Is.EqualTo(newBlock));
        }

        #endregion

        [TestCase(0, 1, ExpectedResult = true)]
        [TestCase(1, 0, ExpectedResult = true)]
        [TestCase(-1, 1, ExpectedResult = false)]
        [TestCase(1, -1, ExpectedResult = false)]
        [TestCase(-1, -1, ExpectedResult = false)]
        [TestCase(5, 1, ExpectedResult = false)]
        [TestCase(1, 5, ExpectedResult = false)]
        public bool IsValidPosition_VariousPositions_ReturnsExpectedResult(int x, int y)
        {
            // Arrange
            var position = new int2(x, y);

            // Act & Assert
            return _data.IsValidPosition(position);
        }
    }
}
