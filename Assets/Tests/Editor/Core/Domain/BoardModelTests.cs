using CardRefactor.Core.Domain;
using NUnit.Framework;
using System;
using Unity.Mathematics;

namespace CardRefactor.Tests.Editor.Core.Domain
{
    public sealed class BoardModelTests
    {
        private BoardData _data;
        private BoardModel _model;
        private readonly int2 _size = new(5, 5);

        [SetUp]
        public void Setup()
        {
            _data = new(_size);
            _model = new BoardModel(_data);
        }

        #region Constructor

        [Test]
        public void Constructor_NullData_ThrowsException()
        {
            // Arrange, Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new BoardModel(null));
            Assert.That(ex.ParamName, Is.EqualTo("data"));
        }

        [Test]
        public void Constructor_ValidData_SetsSizeProperty()
        {
            // Arrange
            var data = new BoardData(_size);

            // Act
            var model = new BoardModel(data);

            // Assert
            Assert.That(model.Size, Is.EqualTo(_size));
        }

        #endregion

        #region GetBlock
        [TestCase(-1, 0)]
        [TestCase(10, 10)]
        public void GetBlock_InvalidPosition_ThrowsException(int x, int y)
        {
            // Arrange
            var pos = new int2(x, y);

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _model.GetBlock(pos));
            Assert.That(ex.ParamName, Is.EqualTo("pos"));
        }

        [Test]
        public void GetBlock_ValidPosition_ReturnsCorrectBlockData()
        {
            // Arrange
            var pos = new int2(2, 3);
            var expectedBlockData = new BlockData(BlockType.Green);
            _data.GetBlockRef(pos) = expectedBlockData;

            // Act
            var retrievedBlockData = _model.GetBlock(pos);

            // Assert
            Assert.That(retrievedBlockData, Is.EqualTo(expectedBlockData));
        }

        #endregion

        #region SetBlock

        [Test]
        public void SetBlock_ValidPosition_UpdatesGridCorrectly()
        {
            // Arrange
            var pos = new int2(2, 2);
            var newBlockData = new BlockData(BlockType.Red);

            // Act
            _model.SetBlock(pos, newBlockData);

            // Assert
            var retrievedBlockData = _data.GetBlockRef(pos);
            Assert.That(retrievedBlockData, Is.EqualTo(newBlockData));
        }

        [TestCase(-1, 0)]
        [TestCase(10, 10)]
        public void SetBlock_InvalidPosition_ThrowsException(int x, int y)
        {
            // Arrange
            var pos = new int2(x, y);
            var newBlockData = new BlockData(BlockType.Blue);

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _model.SetBlock(pos, newBlockData));
            Assert.That(ex.ParamName, Is.EqualTo("pos"));
        }

        #endregion

        #region IsValid

        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(4, 4, ExpectedResult = true)]
        [TestCase(-1, 0, ExpectedResult = false)]
        [TestCase(10, 5, ExpectedResult = false)]
        public bool IsValid_VariousPositon_ReturnsCorrectResult(int x, int y)
        {
            // Arrange
            var pos = new int2(x, y);

            // Act & Assert
            return _model.IsValidPosition(pos);
        }

        #endregion
    }
}
