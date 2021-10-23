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
            player.Setup(x => x.GetMarker()).Returns(MessageHandler.DefaultBoardEmojiMarker.Cross.code);
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

        [Test]
        public void ReturnsFalseWhenGameIsNotOver()
        {
            Assert.IsFalse(this.game.IsOver());
        }

        [Test]
        public void SwapsTurnToNextPlayer()
        {
            Assert.AreEqual(Board.Marks.x.ToString(), this.game.GetTurn());
            this.game.SwapTurn();
            Assert.AreEqual(Board.Marks.o.ToString(), this.game.GetTurn());
        }

        [Test]
        public void PrintsThreeByThreeBoard()
        {
            StringWriter sw = CaptureOutput();
            string[,] board =
            {
                {"1", "2", "3"}, 
                {"4", "5", "6"},
                {"7", "8", "9"}
            };
            this.game.PrintResults();
            string expected = " 01 | 02 | 03 \n--------------\n 04 | 05 | 06 \n--------------\n 07 | 08 | 09 ";
            StringAssert.Contains(expected, sw.ToString());
        }
        
        [Test]
        public void PrintsWinnerOfGame()
        {
            StringWriter sw = CaptureOutput();
            this.game.PrintWinner(this.playerOne);
            StringAssert.Contains($"{MessageHandler.DefaultBoardEmojiMarker.Cross.code} won the game!", sw.ToString());
        }
        
        [Test]
        public void PrintsDrawOutcome()
        {
            StringWriter sw = CaptureOutput();
            this.game.PrintDraw();
            StringAssert.Contains("No one wins!", sw.ToString());
        }
    }
}
