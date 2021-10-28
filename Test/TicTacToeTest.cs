using App.Client;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class TicTacToeTest
    {
        private IUserInterfaceable clientInterface;

        [SetUp]
        public void Init()
        {
            Mock<IUserInterfaceable> mock = new Mock<IUserInterfaceable>();
            mock.Setup(m => m.Run(It.IsAny<IUserInterfaceable>()));
            clientInterface = mock.Object;
        }

        [Test]
        public void RunIsCalledOnce()
        {
            clientInterface.Run(clientInterface);
            Mock.Get(clientInterface).Verify(x =>
                x.Run(It.IsAny<IUserInterfaceable>()), Times.Exactly(1));
        }
    }
}
