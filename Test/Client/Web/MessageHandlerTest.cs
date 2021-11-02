using App.Client.CLI;
using App.Client.Web;
using App.UI.Message;
using NUnit.Framework;

namespace Test.Client.Web
{
    [TestFixture]
    public class MessageHandlerTest
    {
        private App.Client.Web.Web.MessageHandler messageHandler;

        [SetUp]
        public void Init()
        {
            messageHandler = new App.Client.Web.Web.MessageHandler();
        }
        
        [Test]
        public void ReturnsGreeting()
        {
            messageHandler.Print(StaticMessage.Greeting);
            StringAssert.Contains("Welcome to Tic-Tac-Toe!", messageHandler.Message);
        }
    }
}
