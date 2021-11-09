using System;
using System.Linq;
using TicTacToeEngine;
using TicTacToeEngine.UI;
using TicTacToeEngine.UI.Message;
using Moq;
using NUnit.Framework;

namespace TicTacToeEngineTest.UI
{
    [TestFixture]
    public class ValidatorTest
    {
        [Test]
        public void ReturnsTrueIfGameModeIsValid()
        {
            Assert.IsTrue(Validator.IsGameModeValid("1"));
        }
        
        [Test]
        public void ReturnsFalseIfGameModeIsInvalid()
        {
            Assert.IsFalse(Validator.IsGameModeValid(""));
            Assert.IsFalse(Validator.IsGameModeValid("x"));
            Assert.IsFalse(Validator.IsGameModeValid("4"));
        }
        
        [Test]
        public void ReturnsTrueIfBoardSizeIsValid()
        {
            Assert.IsTrue(Validator.IsBoardSizeValid(""));
            Assert.IsTrue(Validator.IsBoardSizeValid("3"));
            Assert.IsTrue(Validator.IsBoardSizeValid("4"));
            Assert.IsTrue(Validator.IsBoardSizeValid("5"));
        }
        
        [Test]
        public void ReturnsFalseIfBoardSizeIsInvalid()
        {
            Assert.IsFalse(Validator.IsBoardSizeValid("1"));
            Assert.IsFalse(Validator.IsBoardSizeValid("2"));
            Assert.IsFalse(Validator.IsBoardSizeValid("6"));
            Assert.IsFalse(Validator.IsBoardSizeValid("7"));
        }
        
        [Test]
        public void ReturnsTrueIfMarkerIsValid()
        {
            Assert.IsTrue(Validator.IsMarkerValid(DefaultBoardEmojiMarker.Circle.code));
        }
        
        [Test]
        public void ReturnsFalseIfMarkerIsInvalid()
        {
            Assert.IsFalse(Validator.IsMarkerValid("x"));
            Assert.IsFalse(Validator.IsMarkerValid("o"));
        }
        
        [Test]
        public void ReturnsTrueIfInputCanBeParsedIntoAPositiveInteger()
        {
            Assert.IsTrue(Validator.IsInputAPositiveInteger("0"));
            Assert.IsTrue(Validator.IsInputAPositiveInteger("12"));
        }
        
        [Test]
        public void ReturnsFalseIfInputCannotBeParsedIntoAPositiveInteger()
        {
            Assert.IsFalse(Validator.IsInputAPositiveInteger("hello"));
            Assert.IsFalse(Validator.IsInputAPositiveInteger(""));
        }
        
        private Board GetEmptyThreeByThreeBoard()
        {
            var mock = new Mock<Board>()
            {
                CallBase = true
            };
            int boardLength = (int)Math.Pow((int)Board.Dimensions.ThreeByThree, 2);
            mock.Setup(x => x.IsValidField(It.IsIn(Enumerable.Range(0, boardLength))))
                .Returns(true);
            mock.Setup(x => x.IsEmptyField(It.IsIn(Enumerable.Range(0, boardLength))))
                .Returns(true);
            return mock.Object;
        }
        
        private Board GetFilledThreeByThreeBoard()
        {
            var mock = new Mock<Board>()
            {
                CallBase = true
            };
            
            int boardLength = (int) Math.Pow((int) Board.Dimensions.ThreeByThree, 2);
            mock.Setup(x => x.IsValidField(It.IsIn(Enumerable.Range(0, boardLength))))
                .Returns(true);
            mock.Setup(x => x.IsEmptyField(It.IsIn(Enumerable.Range(0, boardLength))))
                .Returns(false);
            return mock.Object;
        }
        
        [Test]
        public void ReturnsTrueIfMoveIsWithinBounds()
        {
            Board threeByThreeBoard = GetEmptyThreeByThreeBoard();
            Assert.IsTrue(Validator.IsMoveWithinBounds(threeByThreeBoard, 0));
            Assert.IsTrue(Validator.IsMoveWithinBounds(threeByThreeBoard, 8));
        }
        
        [Test]
        public void ReturnsFalseIfMoveIsWithinBounds()
        {
            Board threeByThreeBoard = GetEmptyThreeByThreeBoard();
            Assert.IsFalse(Validator.IsMoveWithinBounds(threeByThreeBoard, -1));
            Assert.IsFalse(Validator.IsMoveWithinBounds(threeByThreeBoard, 9));
        }

        [Test]
        public void ReturnsTrueIfMoveHasNotAlreadyBeenTaken()
        {
            Board threeByThreeBoard = GetEmptyThreeByThreeBoard();
            Assert.IsTrue(Validator.IsMoveAvailable(threeByThreeBoard, 0));
            Assert.IsTrue(Validator.IsMoveAvailable(threeByThreeBoard, 8));
        }
        
        [Test]
        public void ReturnsFalseIfMoveHasAlreadyBeenTaken()
        {
            Board threeByThreeBoard = GetFilledThreeByThreeBoard();
            Assert.IsFalse(Validator.IsMoveAvailable(threeByThreeBoard, 2));
        }
    }
}
