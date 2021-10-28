using System;
using System.Collections.Generic;
using System.IO;
using App;
using App.Client;
using App.Client.CLI;
using App.Players;
using App.UI.Message;
using Moq;
using NUnit.Framework;

namespace Test.Client.CLI
{
    [TestFixture]
    public class CommandLineInterfaceTest
    {
        private CommandLineInterface commandLineInterface;
        
        [SetUp]
        public void Init()
        {
            var playerOne = new Mock<Player>(DefaultBoardEmojiMarker.Cross.code);
            var playerTwo = new Mock<Player>(DefaultBoardEmojiMarker.Circle.code);
            Mock<CommandLineInterface> commandLineInterface = new Mock<CommandLineInterface>();
            commandLineInterface.Setup(x => x.SetupGame())
                .Returns(new Game(playerOne.Object, playerTwo.Object, Board.Dimensions.ThreeByThree));
            this.commandLineInterface = commandLineInterface.Object;
        }

        private StringWriter CaptureOutput()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            return sw;
        }
        
        [Test]
        public void BoardInvocation_ReturnsNull()
        {
            Assert.IsNull(commandLineInterface.Board());
        }
        
        [Test]
        public void ReturnsAThreeByThreeBoardSize()
        {
            TestHelper.TestHelper.SetInput("3\n");
            Board.Dimensions boardSize = this.commandLineInterface.GetBoardSize();
            Assert.AreEqual(Board.Dimensions.ThreeByThree, boardSize);
        }
        
        [Test]
        public void ReturnsPlayerOneMaker()
        {
            TestHelper.TestHelper.SetInput($"{DefaultBoardEmojiMarker.Cross.code}\n");
            string expected = DefaultBoardEmojiMarker.Cross.code;
            string actual = this.commandLineInterface.GetPlayerOneMarker(true);
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void ReturnsPlayerTwoMaker()
        {
            TestHelper.TestHelper.SetInput($"{DefaultBoardEmojiMarker.Circle.code}\n");
            string expected = DefaultBoardEmojiMarker.Circle.code;
            string actual = this.commandLineInterface.GetPlayerTwoMarker();
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void PrintsLineBreak()
        {
            StringWriter sw = CaptureOutput();
            this.commandLineInterface.PrintLineBreak();
            string expected = "\n";
            StringAssert.Contains(expected, sw.ToString());
        }

        [Test]
        public void PrintsEmptyThreeByThreeBoard()
        {
            StringWriter sw = CaptureOutput();
            this.commandLineInterface.Board();
            string expected = " 01 | 02 | 03 \n--------------\n 04 | 05 | 06 \n--------------\n 07 | 08 | 09 ";
            StringAssert.Contains(expected, sw.ToString());
        }
    }
}
