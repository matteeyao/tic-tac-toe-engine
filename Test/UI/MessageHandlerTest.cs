using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using System.IO;
using App.Players;
using App.UI;

namespace Test.UI
{
    [TestFixture]
    public class MessageHandlerTest
    { 
        public StringWriter CaptureOutput()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            return sw;
        }
        
        [Test]
        public void PrintsGreeting()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(MessageHandler.StaticMessage.Greeting);
            StringAssert.Contains("Welcome to Tic-Tac-Toe!", sw.ToString());
        }

        [Test]
        public void PrintsOptionalGameModes()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(MessageHandler.StaticMessage.GameModes);
            StringAssert.Contains("(1) Play against a friend\n", sw.ToString());
        }

        [Test]
        public void PrintsRequestToChooseFromOneOfTheGameModes()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(MessageHandler.StaticMessage.RequestToChooseGameMode);
            StringAssert.Contains("Choose from one of the above options: ", sw.ToString());
        }
        
        [Test]
        public void IfInputIsInvalid_PrintsErrorAndRequestToChooseFromOneOfTheGameModes()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(MessageHandler.StaticMessage.RequestToChooseGameModeAfterInvalidInput);
            // StringAssert.Contains("Invalid option. Choose again from options 1-3: ", sw.ToString());
            StringAssert.Contains("Invalid option. Choose option 1: ", sw.ToString());
        }
        
        [Test] 
        public void PrintsRequestToEnterBoardDimensionBetweenThreeAndFive()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(MessageHandler.StaticMessage.RequestToInputBoardSize);
            StringAssert.Contains("Enter board size 3, 4, or 5 (Press enter to default to 3): ", sw.ToString());
        }
        
        [Test]
        public void IfInputIsInvalid_PrintsErrorAndRequestToEnterBoardDimensionBetweenThreeAndFive()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(MessageHandler.StaticMessage.RequestToInputBoardSizeAfterInvalidInput);
            StringAssert.Contains("Invalid board size. Enter board size 3, 4, or 5: ", sw.ToString());
        }

        [Test]
        public void PrintsRequestForPlayerOneToInputHerEmoji()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(MessageHandler.DynamicMessage.RequestForPlayerOnesMarker(false));
            StringAssert.Contains("Enter player one's emoji mark (Hit enter to default to \u274C): ", sw.ToString());
        }
        
        [Test]
        public void PrintsRequestForPlayerTwoToInputHerEmoji()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(MessageHandler.StaticMessage.RequestForPlayerTwosMarker);
            StringAssert.Contains("Enter player two's emoji mark (Hit enter to default to \u2B55): ", sw.ToString());
        }

        [Test]
        public void PrintsPersonalRequestForPlayerToInputMove()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.PrintRequestForPlayerToInputMove("\u274C", 3);
            StringAssert.Contains("\u274C enter a position 1-9 to mark: ", sw.ToString());
        }

        [Test]
        public void PrintsNoticeOfAnInvalidPositionEntered()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(MessageHandler.StaticMessage.NoticeForInvalidPosition);
            StringAssert.Contains("Invalid position! ", sw.ToString());
        }

        [Test]
        public void PrintsNoticeIfAPositionHasAlreadyBeenTaken()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(MessageHandler.StaticMessage.NoticeIfPositionIsTaken);
            StringAssert.Contains("Position is already taken!\n", sw.ToString());
        }
        
        [Test]
        public void PrintsAcknowledgementOfAWonGameAndWinner()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.PrintDeclarationOfWinner("\u274C");
            StringAssert.Contains("\u274C won the game!", sw.ToString());
        }
                
        [Test]
        public void PrintsAcknowledgementOfGameEndingInADraw()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(MessageHandler.StaticMessage.DeclarationOfDraw);
            StringAssert.Contains("No one wins!", sw.ToString());
        }
        
        private Dictionary<string, Player> GetPlayers()
        {
            var playerOne = new Mock<Player>( );
            var playerTwo = new Mock<Player>( );
            playerOne.Setup(x => x.GetMarker()).Returns("\u274C");
            playerTwo.Setup(x => x.GetMarker()).Returns("\u2B55");
            Dictionary<string, Player> players = new Dictionary<string, Player>()
            {
                {"x", playerOne.Object},
                {"o", playerTwo.Object},
            };
            return players;
        } 

        [Test]
        public void PrintsEmptyThreeByThreeBoard()
        {
            StringWriter sw = CaptureOutput();
            string[,] board =
            {
                {"1", "2", "3"}, 
                {"4", "5", "6"},
                {"7", "8", "9"}
            };
            Dictionary<string, Player> players = GetPlayers();
            MessageHandler.PrintBoard(board, players);
            string expected = " 01 | 02 | 03 \n--------------\n 04 | 05 | 06 \n--------------\n 07 | 08 | 09 ";
            StringAssert.Contains(expected, sw.ToString());
        }
        
        [Test]
        public void PrintsFilledThreeByThreeBoard()
        {
            StringWriter sw = CaptureOutput();
            string[,] board =
            {
                {"o", "x", "o"}, 
                {"x", "o", "x"},
                {"x", "o", "x"}
            };
            Dictionary<string, Player> players = GetPlayers();
            MessageHandler.PrintBoard(board, players);
            string expected = " \u2B55 | \u274C | \u2B55 \n--------------\n \u274C | \u2B55 | \u274C \n--------------\n \u274C | \u2B55 | \u274C ";
            StringAssert.Contains(expected, sw.ToString());
        }
        
        [Test]
        public void PrintsEmptyFourByFourBoard()
        {
            StringWriter sw = CaptureOutput();
            string[,] board =
            {
                {"1", "2", "3", "4"}, 
                {"5", "6", "7", "8"}, 
                {"9", "10", "11", "12"},
                {"13", "14", "15", "16"}
            };
            Dictionary<string, Player> players = GetPlayers();
            MessageHandler.PrintBoard(board, players);
            string expected = " 01 | 02 | 03 | 04 \n-------------------\n 05 | 06 | 07 | 08 \n-------------------\n 09 | 10 | 11 | 12 \n-------------------\n 13 | 14 | 15 | 16 ";
            StringAssert.Contains(expected, sw.ToString());
        }
        
        [Test]
        public void PrintsFilledFourByFourBoard()
        {
            StringWriter sw = CaptureOutput();
            string[,] board =
            {
                {"x", "x", "x", "o"}, 
                {"o", "o", "x", "x"},
                {"o", "x", "x", "o"},
                {"x", "x", "x", "x"}
            };
            Dictionary<string, Player> players = GetPlayers();
            MessageHandler.PrintBoard(board, players);
            string expected = " \u274C | \u274C | \u274C | \u2B55 \n-------------------\n \u2B55 | \u2B55 | \u274C | \u274C \n-------------------\n \u2B55 | \u274C | \u274C | \u2B55 \n-------------------\n \u274C | \u274C | \u274C | \u274C ";
            StringAssert.Contains(expected, sw.ToString());
        }
    }
}
