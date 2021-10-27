using App.Client.Web;
using App.UI.Message;
using NUnit.Framework;

namespace Test.Client.Web
{
    [TestFixture]
    public class MessageHandlerTest
    {
        [Test]
        public void ReturnsGreeting()
        {
            string content = MessageHandler.Print(StaticMessage.Greeting);
            StringAssert.Contains("Welcome to Tic-Tac-Toe!", content);
        }
    }
}
