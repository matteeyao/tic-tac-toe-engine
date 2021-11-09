using TicTacToeEngine.Client;
using TicTacToeEngine.Client.Web;
using TicTacToeEngine.UI.Message;
using NUnit.Framework;

namespace Test.Client.Web
{
    [TestFixture]
    public class MessageHandlerTest
    {
        private IClient.Interactable messageHandler;

        [SetUp]
        public void Init()
        {
            messageHandler = new MessageHandler();
        }
        
        [Test]
        public void ReturnsGreeting()
        {
            messageHandler.Print(StaticMessage.Greeting);
            StringAssert.Contains("Welcome to Tic-Tac-Toe!", messageHandler.Message);
        }
    }
}
