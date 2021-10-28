using App;
using App.Client;
using App.Client.CLI;
using App.Players;
using App.UI.Message;
using Moq;
using NUnit.Framework;

namespace Test.Players
{
    [TestFixture]
    public class PlayerTest
    {
        private IUserInterfaceable client;
        private string marker;
        private Player player;
        
        [SetUp]
        public void Init()
        {
            this.client = Mock.Of<IUserInterfaceable>();
            this.marker = DefaultBoardEmojiMarker.Cross.code;
            this.player = SetUpMockPlayer();
        }

        private Player SetUpMockPlayer()
        {
            Mock<Player> player = new Mock<Player>(this.marker)
            {
                CallBase = true
            };
            player.Setup(x => x.Move(It.IsAny<IUserInterfaceable>(), It.IsAny<Game>(), It.IsAny<string>())).Returns(0);
            return player.Object;
        }

        [Test]
        public void ReturnsMarker()
        {
            Assert.AreEqual(DefaultBoardEmojiMarker.Cross.code, this.player.GetMarker());
        }
        
        [Test]
        public void ReturnsPosition()
        {
            Game game = new Game(SetUpMockPlayer(), SetUpMockPlayer(), Board.Dimensions.ThreeByThree);
            Assert.AreEqual(0, this.player.Move(client, game, Board.Marks.x.ToString()));
        }
    }
}
