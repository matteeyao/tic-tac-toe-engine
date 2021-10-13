using App;
using App.Players;
using App.UI;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class GameTest
    {
        private Player playerOne;
        private Player playerTwo;
        private Game game;

        [SetUp]
        public void Init()
        {
            this.playerOne = new Mock<Player>(MessageHandler.DefaultBoardEmojiMarker.Cross.code)
            {
                CallBase = true
            }.Object;
            this.playerTwo = new Mock<Player>(MessageHandler.DefaultBoardEmojiMarker.Circle.code)
            {
                CallBase = true
            }.Object;
            this.game = new Game(playerOne, playerTwo, Board.Dimensions.ThreeByThree);
        }
    }
}
