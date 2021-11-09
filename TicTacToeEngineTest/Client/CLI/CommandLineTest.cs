using System;
using System.Collections.Generic;
using System.IO;
using TicTacToeEngine;
using TicTacToeEngine.Client;
using TicTacToeEngine.Client.CLI;
using TicTacToeEngine.Players;
using TicTacToeEngine.UI.Message;
using Moq;
using NUnit.Framework;

namespace Test.Client.CLI
{
    [TestFixture]
    public class CommandLineTest
    {
        private CommandLine _commandLine;
        
        [SetUp]
        public void Init()
        {
            var playerOne = new Mock<Player>(DefaultBoardEmojiMarker.Cross.code);
            var playerTwo = new Mock<Player>(DefaultBoardEmojiMarker.Circle.code);
            Mock<CommandLine> commandLineInterface = new Mock<CommandLine>();
            commandLineInterface.Setup(x => x.SetupGame())
                .Returns(new Game(playerOne.Object, playerTwo.Object, Board.Dimensions.ThreeByThree));
            this._commandLine = commandLineInterface.Object;
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
            Assert.IsNull(_commandLine.Board());
        }
        
        [Test]
        public void ReturnsAThreeByThreeBoardSize()
        {
            TestHelper.TestHelper.SetInput("3\n");
            Board.Dimensions boardSize = this._commandLine.GetBoardSize();
            Assert.AreEqual(Board.Dimensions.ThreeByThree, boardSize);
        }
        
        [Test]
        public void ReturnsPlayerOneMaker()
        {
            TestHelper.TestHelper.SetInput($"{DefaultBoardEmojiMarker.Cross.code}\n");
            string expected = DefaultBoardEmojiMarker.Cross.code;
            string actual = this._commandLine.GetPlayerOneMarker(true);
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void ReturnsPlayerTwoMaker()
        {
            TestHelper.TestHelper.SetInput($"{DefaultBoardEmojiMarker.Circle.code}\n");
            string expected = DefaultBoardEmojiMarker.Circle.code;
            string actual = this._commandLine.GetPlayerTwoMarker();
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void PrintsLineBreak()
        {
            StringWriter sw = CaptureOutput();
            this._commandLine.PrintLineBreak();
            string expected = "\n";
            StringAssert.Contains(expected, sw.ToString());
        }

        [Test]
        public void PrintsEmptyThreeByThreeBoard()
        {
            StringWriter sw = CaptureOutput();
            this._commandLine.Board();
            string expected = " 01 | 02 | 03 \n--------------\n 04 | 05 | 06 \n--------------\n 07 | 08 | 09 ";
            StringAssert.Contains(expected, sw.ToString());
        }
    }
}
