using System;
using App.Players;
using App.UI;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            int mode = Prompt.GetGameMode();
            PrintLineBreak();
            switch (mode)
            {
                case 1:
                    RunCustomGame();
                    break;
            }
        }

        static void RunCustomGame()
        {
            Board.Dimensions boardSize = GetBoardSize();
            string playerOneMarker = GetPlayerOneMarker(false);
            string playerTwoMarker = GetPlayerTwoMarker();
            Game game = new Game(new Human(playerOneMarker), new Human(playerTwoMarker), boardSize);
            game.Run();
        }

        private static Board.Dimensions GetBoardSize()
        {
            Board.Dimensions boardSize = Prompt.GetBoardSize();
            PrintLineBreak();
            return boardSize;
        }

        private static string GetPlayerOneMarker(bool isOpponentComputer)
        {
            return Prompt.GetPlayerOneMarker(isOpponentComputer);
        }

        private static string GetPlayerTwoMarker()
        {
            return Prompt.GetPlayerTwoMarker();
        }

        private static void PrintLineBreak()
        {
            Console.WriteLine();
        }
    }
}
