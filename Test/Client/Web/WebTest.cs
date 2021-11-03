using System;
using System.Linq;
using App;
using App.Client;
using App.Client.Web;
using App.Players;
using App.UI;
using App.UI.Message;
using Moq;
using NUnit.Framework;

namespace Test.Client.Web
{
    [TestFixture]
    public class WebTest
    {
        private App.Client.Web.Web _web;

        [SetUp]
        public void Init()
        {
            _web = new App.Client.Web.Web();
        }

        [Test]
        public void ReturnsWebMessageHandler()
        {
            IClient.Interactable messageHandler = _web.GetMessageHandler();
            Assert.IsInstanceOf<IClient.Interactable>(messageHandler);
        }

        [Test]
        public void ReturnsPrompt()
        {
            Prompt prompt = _web.GetPrompt();
            Assert.IsInstanceOf<Prompt>(prompt);
        }

        [Test]
        public void ReturnsMessage()
        {
            _web.GetMessageHandler().Print(StaticMessage.Greeting);
            StringAssert.Contains("Welcome to Tic-Tac-Toe!", _web.GetMessage());
        }

        [Test]
        public void WhenInputPropertyIsChanged_ReturnsTrue()
        {
            // Assert.IsTrue(new WebInterface.MessageHandler().Read());
        }
        
        [Test]
        public void ReturnsBoardGrid()
        {
            string[] board = _web.Board();
            int boardSize = (int) Math.Pow((int) Board.Dimensions.ThreeByThree, 2);
            string[] expected = Enumerable.Range(1, boardSize).Select(i => i.ToString()).ToArray();
            Assert.AreEqual(expected, board);
        }
        
        [Test]
        public void RunIsInvokedOnGameAtLeastOnce()
        {
            Game game = SetupMockGame();
            App.Client.Web.Web webWithMockedGame = new App.Client.Web.Web(game);
            webWithMockedGame.Run(webWithMockedGame, "0");
            Mock.Get(game).Verify(x =>
                x.InvokeTurn(It.IsAny<IClient>(), "0"), Times.AtLeast(1));
        }
        
        private Game SetupMockGame()
        {
            Mock<Game> mockGame = new Mock<Game>(It.IsAny<Player>(), It.IsAny<Player>(), Board.Dimensions.ThreeByThree);
            mockGame.Setup(game => game.InvokeTurn(It.IsAny<IClient>(), It.IsAny<string>()));
            mockGame.Setup(game => game.PromptMoveMessage(It.IsAny<IClient>()));
            return mockGame.Object;
        }
    }
}