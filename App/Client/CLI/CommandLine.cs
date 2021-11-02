using System;
using System.Threading.Tasks;
using App.Players;
using App.UI;
using App.UI.Message;

namespace App.Client.CLI
{
    public class CommandLine : IRunnable
    {
        public class MessageHandler : IRunnable.Interactable
        {
            private string message;
            public string Message
            {
                get => message;
                set => message = value;
            }
            private string input;
            public string Input
            {
                get => input;
                set => input = value;
            }
            
            public void Print(IPrintable message)
            {
                Message = message.GetMessage();
                Console.Write(Message);
            }
    
            public string Read()
            {
                Input = Console.ReadLine();
                return Input;
            }
        }

        private static MessageHandler messageHandler;
        private static Prompt prompt;
        private Game game;

        public CommandLine()
        {
            messageHandler = new MessageHandler();
            prompt  = new Prompt(messageHandler);
            game = SetupGame();
        }

        public IRunnable.Interactable GetMessageHandler()
        {
            return messageHandler;
        }

        public Prompt GetPrompt()
        {
            return prompt;
        }
        
        public void Run(IRunnable client)
        {
            this.game.Run(client);
        }

        public string[] Board()
        {
            this.game.PrintBoard(messageHandler);
            return null;
        }
        
        public virtual Game SetupGame()
        {
            int mode = prompt.GetGameMode();
            PrintLineBreak();
            switch (mode)
            {
                case 1:
                    return SetupCustomGame();
                default:
                    return null;
            }
        }
        
        public Game SetupCustomGame()
        {
            Board.Dimensions boardSize = GetBoardSize();
            string playerOneMarker = GetPlayerOneMarker(false);
            string playerTwoMarker = GetPlayerTwoMarker();
            return new Game(new Human(playerOneMarker), new Human(playerTwoMarker), boardSize);
        }

        public Board.Dimensions GetBoardSize()
        {
            Board.Dimensions boardSize = prompt.GetBoardSize();
            PrintLineBreak();
            return boardSize;
        }

        public string GetPlayerOneMarker(bool isOpponentComputer)
        {
            return prompt.GetPlayerOneMarker(isOpponentComputer);
        }

        public string GetPlayerTwoMarker()
        {
            return prompt.GetPlayerTwoMarker();
        }

        public void PrintLineBreak()
        {
            Console.WriteLine();
        }
    }
}
