using App;
using App.Client;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class TicTacToeTest
    { 
        private IClient client;
        private TicTacToe ticTacToe;

        [SetUp]
        public void Init()
        {
            Mock<IClient> mock = new Mock<IClient>();
            mock.Setup(m => m.Run(It.IsAny<IClient>(), null));
            client = mock.Object;
            ticTacToe = new TicTacToe(client);
        }

        [Test]
        public void RunIsCalledOnce()
        {
            ticTacToe.Run();
            Mock.Get(client).Verify(x =>
                x.Run(It.IsAny<IClient>(), null), Times.Exactly(1));
        }
    }
}
