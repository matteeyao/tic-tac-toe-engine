using TicTacToeEngine;
using TicTacToeEngine.Client;
using TicTacToeEngine.Client.CLI;
using TicTacToeEngine.Players;
using TicTacToeEngine.UI.Message;
using Moq;
using NUnit.Framework;

namespace TicTacToeEngineTest.Players
{
    [TestFixture]
    public class PlayerTest
    {
        private IClient client;
        private string marker;
        private Player player;
        
        [SetUp]
        public void Init()
        {
            this.client = Mock.Of<IClient>();
            this.marker = DefaultBoardEmojiMarker.Cross.code;
            this.player = SetUpMockPlayer();
        }

        private Player SetUpMockPlayer()
        {
            Mock<Player> player = new Mock<Player>(this.marker)
            {
                CallBase = true
            };
            player.Setup(x => x.Move(
                It.IsAny<IClient>(),
                It.IsAny<Game>(),
                It.IsAny<string>(),
                null)).Returns(0);
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
            Assert.AreEqual(0, this.player.Move(client, game, Board.Marks.x.ToString(), null));
        }
    }
}
