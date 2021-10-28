using System;
using App.Players;
using App.UI;
using App.UI.Message;

namespace App.Client.CLI
{
    public class CommandLineInterface : IUserInterfaceable
    {
        public class MessageHandler : IUserInterfaceable.Interactable
        {
            public string Print(IPrintable message)
            {
                Console.Write(message.GetMessage());
                return null;
            }
    
            public string Read(string input = null)
            {
                return Console.ReadLine();
            }
        }

        private static MessageHandler messageHandler;
        private static Prompt prompt;
        private Game game;

        public CommandLineInterface()
        {
            messageHandler = new MessageHandler();
            prompt  = new Prompt(messageHandler);
            game = SetupGame();
        }

        public IUserInterfaceable.Interactable GetMessageHandler()
        {
            return messageHandler;
        }

        public Prompt GetPrompt()
        {
            return prompt;
        }
        
        public void Run(IUserInterfaceable client)
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
