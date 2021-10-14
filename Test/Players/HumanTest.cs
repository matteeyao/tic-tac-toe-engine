using App;
using App.Players;
using App.UI;
using Moq;
using NUnit.Framework;

namespace Test.Players
{
    using TestHelper;
    
    [TestFixture]
    public class HumanTest
    {
        private Human human;
        private Player opponent;
        private Game game;

        [SetUp]
        public void Init()
        {
            this.human = new Human(MessageHandler.DefaultBoardEmojiMarker.Cross.code);
            this.opponent = GetOpponent();
            this.game = GetEmptyThreeByThreeGame();
        }

        private Player GetOpponent()
        {
            var opponent = new Mock<Player>(MessageHandler.DefaultBoardEmojiMarker.Circle.code)
            {
                CallBase = true
            };
            return opponent.Object;
        }
        
        private Game GetEmptyThreeByThreeGame()
        {
            var mockGame = new Mock<Game>(this.human, this.opponent, Board.Dimensions.ThreeByThree)
            {
                CallBase = true
            };
            mockGame.Setup(x => x.GetBoard())
                .Returns(new Board());
            return mockGame.Object;
        }
        
        [Test]
        public void ReturnsAZeroBasedIndexUsedToAccessBoard()
        {
            TestHelper.SetInput("1\n");
            string currentTurn = Board.Marks.x.ToString();
            int index = this.human.Move(this.game, currentTurn);
            Assert.AreEqual(0, index);
        }
    }
}