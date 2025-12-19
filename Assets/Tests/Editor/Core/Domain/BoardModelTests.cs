using NUnit.Framework;
using CardRefactor.Core.Domain;

namespace CardRefactor.Tests.Editor.Core.Domain
{
    public class BoardModelTests
    {
        #region Constructor

        [Test]
        public void Constructor_ValidSize_InitializesAllBlocksToNone()
        {
            // Arrange
            int width = 5;
            int height = 5;

            // Act
            var model = new BoardModel(width, height);

            // Assert
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var blockData = model.GetBlock(x, y);
                    Assert.AreEqual(BlockType.None, blockData.Type);
                }
            }
        }

        [TestCase(0, 5)]
        [TestCase(5, -1)]
        public void Constructor_InvalidSize_ThrowsException(int width, int height)
        {
            var ex = Assert.Throws<System.ArgumentOutOfRangeException>(() => new BoardModel(width, height));
            Assert.That(ex.Message, Does.Contain("must be positive"));
        }

        #endregion

        #region GetBlock
        [TestCase(-1, 0)]
        [TestCase(10, 10)]
        public void GetBlock_InvalidCoord_ReturnsNull(int x, int y)
        {
            // Arrange
            int width = 5;
            int height = 5;
            var model = new BoardModel(width, height);

            // Act
            var blockData = model.GetBlock(x, y);

            // Assert
            Assert.IsNull(blockData);
        }

        #endregion

        #region SetBlock

        [Test]
        public void SetBlock_ValidCoordinate_UpdatesGridCorrectly()
        {
            // Arrange
            int width = 5;
            int height = 5;
            var model = new BoardModel(width, height);
            var newBlockData = new BlockData(BlockType.Red);

            // Act
            model.SetBlock(2, 2, newBlockData);

            // Assert
            var retrievedBlockData = model.GetBlock(2, 2);
            Assert.AreEqual(newBlockData, retrievedBlockData);
        }       

        [TestCase(-1, 0)]
        [TestCase(10, 10)]
        public void SetBlock_InvalidCoord_DoesNothing(int x, int y)
        {
            // Arrange
            int width = 5;
            int height = 5;
            var model = new BoardModel(width, height);
            var originalBlockData = model.GetBlock(0, 0);
            var newBlockData = new BlockData(BlockType.Blue);

            // Act
            model.SetBlock(x, y, newBlockData);

            // Assert
            var checkBlockData = model.GetBlock(0, 0);
            Assert.AreEqual(originalBlockData, checkBlockData);
        }

        [Test]
        public void SetBlock_NullBlockData_DoesNothing()
        {
            // Arrange
            int width = 5;
            int height = 5;
            var model = new BoardModel(width, height);
            var originalBlockData = model.GetBlock(1, 1);

            // Act
            model.SetBlock(1, 1, null);

            // Assert
            var checkBlockData = model.GetBlock(1, 1);
            Assert.AreEqual(originalBlockData, checkBlockData);
        }

        #endregion

        #region IsValid

        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(4, 4, ExpectedResult = true)]
        [TestCase(-1, 0, ExpectedResult = false)]
        [TestCase(5, 5, ExpectedResult = false)]
        public bool IsValid_VariousCoordinates_ReturnsCorrectResult(int x, int y)
        {
            // Arrange
            int width = 5;
            int height = 5;
            var model = new BoardModel(width, height);

            // Act & Assert
            return model.IsValid(x, y);
        }

        #endregion
    }
}
