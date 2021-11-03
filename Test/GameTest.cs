using System;
using System.IO;
using App;
using App.Client;
using App.Client.CLI;
using App.Players;
using App.UI;
using App.UI.Message;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class GameTest
    {
        private IClient client;
        private Player playerOne;
        private Player playerTwo;
        private Game game;
        
        private StringWriter CaptureOutput()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            return sw;
        }

        [SetUp]
        public void Init()
        {
            client = SetupClient();
            playerOne = SetUpNewTestPlayer(DefaultBoardEmojiMarker.Cross.code, 0);
            playerTwo = SetUpNewTestPlayer(DefaultBoardEmojiMarker.Circle.code, 1);
            game = new Game(playerOne, playerTwo, Board.Dimensions.ThreeByThree);
        }
        
        private IClient SetupClient()
        {
            Mock<IClient> mock = new Mock<IClient>();
            mock.Setup(m => m.MessageHandler)
                .Returns(new MessageHandler());
            mock.Setup(m => m.Prompt)
                .Returns(new Prompt(new MessageHandler()));
            return mock.Object;
        }

        private Player SetUpNewTestPlayer(string defaultBoardEmojiMarker, int pos)
        {
            Mock<Player> player = new Mock<Player>(defaultBoardEmojiMarker)
            {
                CallBase = true
            };
            player.Setup(x => x.Move(
                It.IsAny<IClient>(),
                It.IsAny<Game>(),
                It.IsIn(Board.Marks.o.ToString(), Board.Marks.x.ToString()),
                null)
            ).Returns(pos);
            player.Setup(x => x.GetMarker()).Returns(defaultBoardEmojiMarker);
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
            this.game.PrintBoard(new MessageHandler());
            string expected = " 01 | 02 | 03 \n--------------\n 04 | 05 | 06 \n--------------\n 07 | 08 | 09 ";
            StringAssert.Contains(expected, sw.ToString());
        }

        private Game SetUpGameWithTiedEndgame()
        {
            game.GetBoard().SetField(2, Board.Marks.x.ToString());
            game.GetBoard().SetField(3, Board.Marks.x.ToString());
            game.GetBoard().SetField(4, Board.Marks.o.ToString());
            game.GetBoard().SetField(5, Board.Marks.o.ToString());
            game.GetBoard().SetField(6, Board.Marks.o.ToString());
            game.GetBoard().SetField(7, Board.Marks.x.ToString());
            game.GetBoard().SetField(8, Board.Marks.x.ToString());
            return game;
        }

        [Test]
        public void IfGameEndsInDraw_PrintsDrawResult()
        {
            StringWriter sw = CaptureOutput();
            SetUpGameWithTiedEndgame().Run(client);
            StringAssert.Contains("No one wins!", sw.ToString());
        }
        
        private Game SetUpGameWithAnEndgameThatHasAWinner()
        {
            game.GetBoard().SetField(2, Board.Marks.o.ToString());
            game.GetBoard().SetField(4, Board.Marks.x.ToString());
            game.GetBoard().SetField(8, Board.Marks.x.ToString());
            return game;
        }
        
        [Test]
        public void IfGameEndsWithAWinner_PrintsWinnerResult()
        {
            StringWriter sw = CaptureOutput();
            SetUpGameWithAnEndgameThatHasAWinner().Run(client);
            StringAssert.Contains($"{DefaultBoardEmojiMarker.Cross.code} won the game!", sw.ToString());
        }
    }
}
