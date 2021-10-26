using System;
using System.IO;
using App;
using App.UI;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class CommandLineTest
    {
        private StringWriter CaptureOutput()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            return sw;
        }
        
        [Test]
        public void ReturnsAThreeByThreeBoardSize()
        {
            TestHelper.TestHelper.SetInput("3\n");
            Board.Dimensions boardSize = CommandLine.GetBoardSize();
            Assert.AreEqual(Board.Dimensions.ThreeByThree, boardSize);
        }
        
        [Test]
        public void ReturnsPlayerOneMaker()
        {
            TestHelper.TestHelper.SetInput($"{MessageHandler.DefaultBoardEmojiMarker.Cross.code}\n");
            string expected = MessageHandler.DefaultBoardEmojiMarker.Cross.code;
            string actual = CommandLine.GetPlayerOneMarker(true);
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void ReturnsPlayerTwoMaker()
        {
            TestHelper.TestHelper.SetInput($"{MessageHandler.DefaultBoardEmojiMarker.Circle.code}\n");
            string expected = MessageHandler.DefaultBoardEmojiMarker.Circle.code;
            string actual = CommandLine.GetPlayerTwoMarker();
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void PrintsLineBreak()
        {
            StringWriter sw = CaptureOutput();
            CommandLine.PrintLineBreak();
            string expected = "\n";
            StringAssert.Contains(expected, sw.ToString());
        }
    }
}
