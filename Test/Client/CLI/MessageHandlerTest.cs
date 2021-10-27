using System;
using System.Collections.Generic;
using System.IO;
using App.Client.CLI;
using App.Players;
using App.UI.Message;
using Moq;
using NUnit.Framework;

namespace Test.Client.CLI
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
            MessageHandler.Print(StaticMessage.Greeting);
            StringAssert.Contains("Welcome to Tic-Tac-Toe!", sw.ToString());
        }

        [Test]
        public void PrintsOptionalGameModes()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(StaticMessage.GameModes);
            StringAssert.Contains("(1) Play against a friend\n", sw.ToString());
        }

        [Test]
        public void PrintsRequestToChooseFromOneOfTheGameModes()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(StaticMessage.RequestToChooseGameMode);
            StringAssert.Contains("Choose from one of the above options: ", sw.ToString());
        }
        
        [Test]
        public void IfInputIsInvalid_PrintsErrorAndRequestToChooseFromOneOfTheGameModes()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(StaticMessage.RequestToChooseGameModeAfterInvalidInput);
            // StringAssert.Contains("Invalid option. Choose again from options 1-3: ", sw.ToString());
            StringAssert.Contains("Invalid option. Choose option 1: ", sw.ToString());
        }
        
        [Test] 
        public void PrintsRequestToEnterBoardDimensionBetweenThreeAndFive()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(StaticMessage.RequestToInputBoardSize);
            StringAssert.Contains("Enter board size 3, 4, or 5 (Press enter to default to 3): ", sw.ToString());
        }
        
        [Test]
        public void IfInputIsInvalid_PrintsErrorAndRequestToEnterBoardDimensionBetweenThreeAndFive()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(StaticMessage.RequestToInputBoardSizeAfterInvalidInput);
            StringAssert.Contains("Invalid board size. Enter board size 3, 4, or 5: ", sw.ToString());
        }

        [Test]
        public void PrintsRequestForPlayerOneToInputHerEmoji()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(DynamicMessage.RequestForPlayerOnesMarker(false));
            StringAssert.Contains("Enter player one's emoji mark (Hit enter to default to \u274C): ", sw.ToString());
        }
        
        [Test]
        public void PrintsRequestForPlayerTwoToInputHerEmoji()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(StaticMessage.RequestForPlayerTwosMarker);
            StringAssert.Contains("Enter player two's emoji mark (Hit enter to default to \u2B55): ", sw.ToString());
        }

        [Test]
        public void PrintsPersonalRequestForPlayerToInputMove()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(DynamicMessage.RequestForPlayerToInputMove("\u274C", 3));
            StringAssert.Contains("\u274C enter a position 1-9 to mark: ", sw.ToString());
        }

        [Test]
        public void PrintsNoticeOfAnInvalidPositionEntered()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(StaticMessage.NoticeForInvalidPosition);
            StringAssert.Contains("Invalid position! ", sw.ToString());
        }

        [Test]
        public void PrintsNoticeIfAPositionHasAlreadyBeenTaken()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(StaticMessage.NoticeIfPositionIsTaken);
            StringAssert.Contains("Position is already taken!\n", sw.ToString());
        }
        
        [Test]
        public void PrintsAcknowledgementOfAWonGameAndWinner()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(DynamicMessage.DeclarationOfWinner(
                DefaultBoardEmojiMarker.Cross.code
            ));
            StringAssert.Contains("\u274C won the game!", sw.ToString());
        }
                
        [Test]
        public void PrintsAcknowledgementOfGameEndingInADraw()
        {
            StringWriter sw = CaptureOutput();
            MessageHandler.Print(StaticMessage.DeclarationOfDraw);
            StringAssert.Contains("No one wins!", sw.ToString());
        }
    }
}
