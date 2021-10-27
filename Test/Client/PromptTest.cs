using App;
using App.Client;
using App.UI;
using App.UI.Message;
using NUnit.Framework;

namespace Test.Client
{
    [TestFixture]
    public class PromptTest
    {
        private Board threeByThreeBoard;
        private Board fourByFourBoard;
        private Board fiveByFiveBoard;
        private string crossMarker;
        
        [SetUp]
        public void Init()
        {
            this.threeByThreeBoard = new Board();
            this.fourByFourBoard = new Board(Board.Dimensions.FourByFour);
            this.fiveByFiveBoard = new Board(Board.Dimensions.FiveByFive);
            this.crossMarker = DefaultBoardEmojiMarker.Cross.code;
        }
        
        [Test]
        public void ReturnsAGameMode()
        {
            TestHelper.TestHelper.SetInput("1\n");
            int gameMode = Prompt.GetGameMode();
            Assert.AreEqual(1, gameMode);
        }
        
        [Test]
        public void ReturnsAThreeByThreeBoardSize()
        {
            TestHelper.TestHelper.SetInput("3\n");
            Board.Dimensions boardSize = Prompt.GetBoardSize();
            Assert.AreEqual(Board.Dimensions.ThreeByThree, boardSize);
        }
        
        [Test]
        public void ReturnsAFourByFourBoardSize()
        {
            TestHelper.TestHelper.SetInput("4\n");
            Board.Dimensions boardSize = Prompt.GetBoardSize();
            Assert.AreEqual(Board.Dimensions.FourByFour, boardSize);
        }
        
        [Test]
        public void ReturnsAFiveByFiveBoardSize()
        {
            TestHelper.TestHelper.SetInput("5\n");
            Board.Dimensions boardSize = Prompt.GetBoardSize();
            Assert.AreEqual(Board.Dimensions.FiveByFive, boardSize);
        }

        [Test]
        public void ReturnsPlayerOneMaker()
        {
            TestHelper.TestHelper.SetInput($"{DefaultBoardEmojiMarker.Cross.code}\n");
            string expected = DefaultBoardEmojiMarker.Cross.code;
            string actual = Prompt.GetPlayerOneMarker();
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void IfInputIsEmpty_ReturnsPlayerOneMaker()
        {
            TestHelper.TestHelper.SetInput("\n");
            string expected = DefaultBoardEmojiMarker.Cross.code;
            string actual = Prompt.GetPlayerOneMarker();
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void ReturnsPlayerTwoMaker()
        {
            TestHelper.TestHelper.SetInput($"{DefaultBoardEmojiMarker.Circle.code}\n");
            string expected = DefaultBoardEmojiMarker.Circle.code;
            string actual = Prompt.GetPlayerTwoMarker();
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void IfInputIsEmpty_ReturnsPlayerTwoMaker()
        {
            TestHelper.TestHelper.SetInput("\n");
            string expected = DefaultBoardEmojiMarker.Circle.code;
            string actual = Prompt.GetPlayerTwoMarker();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IfInputIsOne_AndBoardIsThreeByThree_ReturnsZero()
        {
            TestHelper.TestHelper.SetInput("1\n");
            int index = Prompt.GetMove(crossMarker, threeByThreeBoard);
            Assert.AreEqual(0, index);
        }
        
        [Test]
        public void IfInputIsNine_AndBoardIsThreeByThree_ReturnsEight()
        {
            TestHelper.TestHelper.SetInput("9\n");
            int index = Prompt.GetMove(crossMarker, threeByThreeBoard);
            Assert.AreEqual(8, index);
        }
        
        [Test]
        public void IfInputIsOne_AndBoardIsFourByFour_ReturnsZero()
        {
            TestHelper.TestHelper.SetInput("1\n");
            int index = Prompt.GetMove(crossMarker, fourByFourBoard);
            Assert.AreEqual(0, index);
        }
        
        [Test]
        public void IfInputIsSixteen_AndBoardIsFourByFour_ReturnsFifteen()
        {
            TestHelper.TestHelper.SetInput("16\n");
            int index = Prompt.GetMove(crossMarker, fourByFourBoard);
            Assert.AreEqual(15, index);
        }
        
        [Test]
        public void IfInputIsOne_AndBoardIsFiveByFive_ReturnsZero()
        {
            TestHelper.TestHelper.SetInput("1\n");
            int index = Prompt.GetMove(crossMarker, fiveByFiveBoard);
            Assert.AreEqual(0, index);
        }
        
        [Test]
        public void IfInputIsTwentyFive_AndBoardIsFiveByFive_ReturnsTwentyFour()
        {
            TestHelper.TestHelper.SetInput("25\n");
            int index = Prompt.GetMove(crossMarker, fiveByFiveBoard);
            Assert.AreEqual(24, index);
        }
    }
}
