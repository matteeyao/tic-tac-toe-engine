using App.Client;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class TicTacToeTest
    {
        private IClient clientInterface;

        [SetUp]
        public void Init()
        {
            Mock<IClient> mock = new Mock<IClient>();
            mock.Setup(m => m.Run(It.IsAny<IClient>()));
            clientInterface = mock.Object;
        }

        [Test]
        public void RunIsCalledOnce()
        {
            clientInterface.Run(clientInterface);
            Mock.Get(clientInterface).Verify(x =>
                x.Run(It.IsAny<IClient>()), Times.Exactly(1));
        }
    }
}
