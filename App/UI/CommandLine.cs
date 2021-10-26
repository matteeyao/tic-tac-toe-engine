using System;
using App.Players;
using App.UI;

namespace App
{
    public static class CommandLine
    {
        public static void Run()
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

        public static Board.Dimensions GetBoardSize()
        {
            Board.Dimensions boardSize = Prompt.GetBoardSize();
            PrintLineBreak();
            return boardSize;
        }

        public static string GetPlayerOneMarker(bool isOpponentComputer)
        {
            return Prompt.GetPlayerOneMarker(isOpponentComputer);
        }

        public static string GetPlayerTwoMarker()
        {
            return Prompt.GetPlayerTwoMarker();
        }

        public static void PrintLineBreak()
        {
            Console.WriteLine();
        }
    }
}
