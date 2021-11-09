using System;
using System.Linq;
using TicTacToeEngine;
using TicTacToeEngine.Client;
using TicTacToeEngine.Client.Web;
using TicTacToeEngine.Players;
using TicTacToeEngine.UI;
using TicTacToeEngine.UI.Message;
using Moq;
using NUnit.Framework;

namespace TicTacToeEngineTest.Client.Web
{
    [TestFixture]
    public class WebTest
    {
        private TicTacToeEngine.Client.Web.Web web;

        [SetUp]
        public void Init()
        {
            web = new TicTacToeEngine.Client.Web.Web();
        }

        [Test]
        public void ReturnsWebMessageHandler()
        {
            IClient.Interactable messageHandler = web.GetMessageHandler();
            Assert.IsInstanceOf<IClient.Interactable>(messageHandler);
        }

        [Test]
        public void GivenInputString_ReturnsPosition()
        {
            int pos = web.GetMove(DefaultBoardEmojiMarker.Cross.code, "1");
            Assert.AreEqual(0, pos);
        }
        
        [Test]
        public void ReturnsBoardGrid()
        {
            string[] board = web.Board();
            int boardSize = (int) Math.Pow((int) Board.Dimensions.ThreeByThree, 2);
            string[] expected = Enumerable.Range(1, boardSize).Select(i => i.ToString()).ToArray();
            Assert.AreEqual(expected, board);
        }
        
        [Test]
        public void RunIsInvokedOnGameAtLeastOnce()
        {
            Game game = SetupMockGame();
            TicTacToeEngine.Client.Web.Web webWithMockedGame = new TicTacToeEngine.Client.Web.Web(game);
            webWithMockedGame.Run(webWithMockedGame, "1");
            Mock.Get(game).Verify(x =>
                x.InvokeTurn(It.IsAny<IClient>(), "1"), Times.AtLeast(1));
        }

        [Test]
        public void WhenGameIsStarted_ReturnsFalse()
        {
            Assert.IsFalse(web.IsGameOver());
        }
        
        private Game SetupMockGame()
        {
            Mock<Game> mockGame = new Mock<Game>(It.IsAny<Player>(), It.IsAny<Player>(), Board.Dimensions.ThreeByThree);
            mockGame.Setup(game => game.GetBoard()).Returns(new Board());
            mockGame.Setup(game => game.InvokeTurn(It.IsAny<IClient>(), It.IsAny<string>()));
            mockGame.Setup(game => game.PromptMoveMessage(It.IsAny<IClient>()));
            return mockGame.Object;
        }
    }
}
