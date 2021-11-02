using App;
using App.Client;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class TicTacToeTest
    { 
        private IRunnable client;
        private TicTacToe ticTacToe;

        [SetUp]
        public void Init()
        {
            Mock<IRunnable> mock = new Mock<IRunnable>();
            mock.Setup(m => m.Run(It.IsAny<IRunnable>()));
            client = mock.Object;
            ticTacToe = new TicTacToe(client);
        }

        [Test]
        public void RunIsCalledOnce()
        {
            ticTacToe.Run();
            Mock.Get(client).Verify(x =>
                x.Run(It.IsAny<IRunnable>()), Times.Exactly(1));
        }
    }
}
