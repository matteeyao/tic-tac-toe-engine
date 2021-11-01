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
    public class WebInterfaceTest
    {
        private WebInterface webInterface;

        [SetUp]
        public void Init()
        {
            webInterface = new WebInterface();
        }

        [Test]
        public void ReturnsWebMessageHandler()
        {
            IUserInterfaceable.Interactable messageHandler = webInterface.GetMessageHandler();
            Assert.IsInstanceOf<IUserInterfaceable.Interactable>(messageHandler);
        }

        [Test]
        public void ReturnsPrompt()
        {
            Prompt prompt = webInterface.GetPrompt();
            Assert.IsInstanceOf<Prompt>(prompt);
        }

        [Test]
        public void ReturnsMessage()
        {
            webInterface.GetMessageHandler().Print(StaticMessage.Greeting);
            StringAssert.Contains("Welcome to Tic-Tac-Toe!", webInterface.GetMessage());
        }
        
        [Test]
        public void ReturnsBoardGrid()
        {
            string[] board = webInterface.Board();
            int boardSize = (int) Math.Pow((int) Board.Dimensions.ThreeByThree, 2);
            string[] expected = Enumerable.Range(1, boardSize).Select(i => i.ToString()).ToArray();
            Assert.AreEqual(expected, board);
        }
        
        [Test]
        public void RunIsInvokedOnGameAtLeastOnce()
        {
            Game game = SetupMockGame();
            WebInterface webInterfaceWithMockedGame = new WebInterface(game);
            webInterfaceWithMockedGame.Run(webInterfaceWithMockedGame);
            Mock.Get(game).Verify(x =>
                x.Run(It.IsAny<IUserInterfaceable>()), Times.AtLeast(1));
        }
        
        private Game SetupMockGame()
        {
            Mock<Game> mockGame = new Mock<Game>(It.IsAny<Player>(), It.IsAny<Player>(), Board.Dimensions.ThreeByThree);
            mockGame.Setup(game => game.Run(It.IsAny<IUserInterfaceable>()));
            return mockGame.Object;
        }
    }
}
