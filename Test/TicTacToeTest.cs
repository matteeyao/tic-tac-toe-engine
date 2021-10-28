using App;
using App.Client;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class TicTacToeTest
    { 
        private IUserInterfaceable client;
        private TicTacToe ticTacToe;

        [SetUp]
        public void Init()
        {
            Mock<IUserInterfaceable> mock = new Mock<IUserInterfaceable>();
            mock.Setup(m => m.Run(It.IsAny<IUserInterfaceable>()));
            client = mock.Object;
            ticTacToe = new TicTacToe(client);
        }

        [Test]
        public void RunIsCalledOnce()
        {
            ticTacToe.Run();
            Mock.Get(client).Verify(x =>
                x.Run(It.IsAny<IUserInterfaceable>()), Times.Exactly(1));
        }
    }
}
