using System;
using System.Threading.Tasks;
using TicTacToeEngine.UI.Message;
using TicTacToeEngine.Players;
using TicTacToeEngine.UI;

namespace TicTacToeEngine.Client.CLI
{
    public class CommandLine : IClient
    {
        private static MessageHandler messageHandler;
        public IClient.Interactable MessageHandler { get => messageHandler; }
        private static Prompt prompt;
        public Prompt Prompt { get => prompt; }
        private Game game;

        public CommandLine()
        {
            messageHandler = new MessageHandler();
            prompt  = new Prompt(messageHandler);
            game = SetupGame();
        }
        
        public void Run(IClient client, string input = null)
        {
            this.game.Run(client);
        }

        public int GetMove(string marker, string input)
        {
            Board();
            game.PromptMoveMessage(this);
            return Prompt.GetMove(marker, game.GetBoard());
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
