using App;
using App.Client;
using App.Client.CLI;
using App.Players;
using App.UI;
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
            client = SetupClient();
            human = new Human(DefaultBoardEmojiMarker.Cross.code);
            opponent = GetOpponent();
            game = GetEmptyThreeByThreeGame();
        }

        private IClient SetupClient()
        {
            Mock<IClient> mock = new Mock<IClient>();
            mock.Setup(m => m.Prompt)
                .Returns(new Prompt(new MessageHandler()));
            return mock.Object;
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
            int index = this.human.Move(this.client, this.game, currentTurn, null);
            Assert.AreEqual(0, index);
        }
    }
}
