using System;
using App.Players;
using App.UI;

namespace App.Client.CLI
{
    public class CommandLineInterface : IClient
    {
        private Game game;

        public CommandLineInterface()
        {
            game = SetupGame();
        }
        
        public void Run(IClient client)
        {
            this.game.Run(client);
        }

        public string[] Board()
        {
            this.game.PrintBoard();
            return null;
        }
        
        public virtual Game SetupGame()
        {
            int mode = Prompt.GetGameMode();
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
            Board.Dimensions boardSize = Prompt.GetBoardSize();
            PrintLineBreak();
            return boardSize;
        }

        public string GetPlayerOneMarker(bool isOpponentComputer)
        {
            return Prompt.GetPlayerOneMarker(isOpponentComputer);
        }

        public string GetPlayerTwoMarker()
        {
            return Prompt.GetPlayerTwoMarker();
        }

        public void PrintLineBreak()
        {
            Console.WriteLine();
        }
    }
}
