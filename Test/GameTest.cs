using System;
using System.Collections.Generic;
using System.IO;
using App;
using App.Players;
using App.UI;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class GameTest
    {
        private Player playerOne;
        private Player playerTwo;
        private Game game;
        
        public StringWriter CaptureOutput()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            return sw;
        }

        [SetUp]
        public void Init()
        {
            this.playerOne = SetUpNewTestPlayer(MessageHandler.DefaultBoardEmojiMarker.Cross.code, 0);
            this.playerTwo = SetUpNewTestPlayer(MessageHandler.DefaultBoardEmojiMarker.Circle.code, 1);
            this.game = new Game(playerOne, playerTwo, Board.Dimensions.ThreeByThree);
        }

        private Player SetUpNewTestPlayer(string defaultBoardEmojiMarker, int pos)
        {
            Mock<Player> player = new Mock<Player>(defaultBoardEmojiMarker)
            {
                CallBase = true
            };
            player.Setup(x => x.Move(It.IsAny<Game>(), It.IsAny<string>())).Returns(pos);
            return player.Object;
        }

        [Test]
        public void ReturnsBoardAttribute()
        {
            string[,] boardGrid = new Board(Board.Dimensions.ThreeByThree).GetGrid();
            Assert.AreEqual(boardGrid, this.game.GetBoard().GetGrid());
        }
        
        [Test]
        public void PrintsEmptyThreeByThreeBoard()
        {
            StringWriter sw = CaptureOutput();
            this.game.PrintBoard();
            string expected = " 01 | 02 | 03 \n--------------\n 04 | 05 | 06 \n--------------\n 07 | 08 | 09 ";
            StringAssert.Contains(expected, sw.ToString());
        }

        [Test]
        public void RegistersAPlayersMove()
        {
            string[,] emptyBoard =
            {
                { "1", "2", "3" }, 
                { "4", "5", "6" }, 
                { "7", "8", "9" }
            };
            Assert.AreEqual(emptyBoard, this.game.GetBoard().GetGrid());
            this.game.PlayTurn();
            string[,] boardWithMark =
            {
                { Board.Marks.x.ToString(), "2", "3" }, 
                { "4", "5", "6" }, 
                { "7", "8", "9" }
            };
            Assert.AreEqual(boardWithMark, this.game.GetBoard().GetGrid());
        }
    }
}
