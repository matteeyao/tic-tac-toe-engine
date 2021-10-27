using App;
using App.Client;
using App.Client.CLI;
using App.Players;
using App.UI.Message;
using Moq;
using NUnit.Framework;

namespace Test.Players
{
    using TestHelper;
    
    [TestFixture]
    public class HumanTest
    {
        private IClient client;
        private Human human;
        private Player opponent;
        private Game game;

        [SetUp]
        public void Init()
        {
            this.client = Mock.Of<IClient>();
            this.human = new Human(DefaultBoardEmojiMarker.Cross.code);
            this.opponent = GetOpponent();
            this.game = GetEmptyThreeByThreeGame();
        }

        private Player GetOpponent()
        {
            var opponent = new Mock<Player>(DefaultBoardEmojiMarker.Circle.code)
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
            int index = this.human.Move(this.client, this.game, currentTurn);
            Assert.AreEqual(0, index);
        }
    }
}
