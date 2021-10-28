using App.Client.CLI;
using App.Client.Web;
using App.UI.Message;
using NUnit.Framework;

namespace Test.Client.Web
{
    [TestFixture]
    public class MessageHandlerTest
    {
        private WebInterface.MessageHandler messageHandler;

        [SetUp]
        public void Init()
        {
            messageHandler = new WebInterface.MessageHandler();
        }
        
        [Test]
        public void ReturnsGreeting()
        {
            string content = messageHandler.Print(StaticMessage.Greeting);
            StringAssert.Contains("Welcome to Tic-Tac-Toe!", content);
        }
    }
}
