using App;
using App.Players;
using App.UI;
using Moq;
using NUnit.Framework;

namespace Test.Players
{
    [TestFixture]
    public class PlayerTest
    {
        private string marker;
        private Player player;
        
        [SetUp]
        public void Init()
        {
            this.marker = MessageHandler.DefaultBoardEmojiMarker.Cross.code;
            this.player = SetUpMockPlayer();
        }

        private Player SetUpMockPlayer()
        {
            Mock<Player> player = new Mock<Player>(this.marker)
            {
                CallBase = true
            };
            player.Setup(x => x.Move(It.IsAny<Game>(), It.IsAny<string>())).Returns(0);
            return player.Object;
        }

        [Test]
        public void ReturnsMarker()
        {
            Assert.AreEqual(MessageHandler.DefaultBoardEmojiMarker.Cross.code, this.player.GetMarker());
        }
        
        [Test]
        public void ReturnsPosition()
        {
            Game game = new Game(SetUpMockPlayer(), SetUpMockPlayer(), Board.Dimensions.ThreeByThree);
            Assert.AreEqual(0, this.player.Move(game, Board.Marks.x.ToString()));
        }
    }
}
