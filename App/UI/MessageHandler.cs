using System;
using System.Collections.Generic;
using System.Text;
using App.Players;

namespace App.UI
{
    public static class MessageHandler
    {
        public static void PrintGreeting()
        {
            Console.WriteLine("Welcome to Tic-Tac-Toe!");
        }

        public static void PrintGameModes()
        {
            Console.WriteLine("(1) Play against a friend");
            // Console.WriteLine("(2) Play against an easy competitor");
            // Console.WriteLine("(3) Play against a super computer");
            Console.WriteLine("\n");
        }

        public static void PrintRequestToChooseGameMode()
        {
            Console.Write("Choose from one of the above options: ");
        }

        public static void PrintRequestToChooseGameModeAfterInvalidInput()
        {
            Console.Write("Invalid option. Choose again from options 1-3: ");
        }

        public static void PrintRequestToInputBoardSize()
        {
            Console.Write("Enter board size 3, 4, or 5 (Press enter to default to 3): ");
        }
        
        public static void PrintRequestToInputBoardSizeAfterInvalidInput()
        {
            Console.Write("Invalid board size. Enter board size 3, 4, or 5: ");
        }
        
        public static void PrintRequestForPlayerOnesMarker(bool isOpponentComputer)
        {
            string title = isOpponentComputer ? "your" : "player one's";
            Console.Write($"Enter {title} mark (Hit enter to default to \u274C): ");
        }

        public static void PrintRequestForPlayerTwosMarker()
        {
            Console.Write("Enter player two's mark (Hit enter to default to \u2B55): ");
        }

        public static void PrintRequestForPlayerToInputMove(string mark, int boardDimension)
        {
            Console.Write($"{mark} enter a position 1-{(int) Math.Pow(boardDimension, 2)} to mark: ");
        }

        public static void PrintNoticeForInvalidPosition()
        {
            Console.Write("Invalid position! ");
        }

        public static void PrintNoticeIfPositionIsTaken()
        {
            Console.WriteLine("Position is already taken!\n");
        }

        public static void PrintDeclarationOfWinner(string mark)
        {
            Console.WriteLine($"{mark} won the game!");
        }

        public static void PrintDeclarationOfDraw()
        {
            Console.WriteLine("No one wins!");
        }

        public static string ReadInput()
        {
            return Console.ReadLine();
        }

        public static void PrintBoard(string[,] rows, Dictionary<string, Player> players)
        {
            int rowLength = rows.GetLength(0);
            int colLength = rows.GetLength(1);
            
            string board = "";
            for (int rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                string[] row = new string[rowLength];
                for (int colIdx = 0; colIdx < colLength; colIdx++)
                {
                    row[colIdx] = rows[rowIdx, colIdx];
                }

                board = rowIdx == rowLength - 1 ? 
                    string.Format("{0}\n{1}\n", board, PrintRow(row, players)) : 
                    string.Format("{0}\n{1}\n{2}", board, PrintRow(row, players), PrintRowBorder(rowLength));
            }
            Console.WriteLine(board);
        }
        
        private static string PrintRow(string[] set, Dictionary<string, Player> players)
        {
            string row = " ";
            for (int colIdx = 0; colIdx < set.Length; colIdx++)
            {
                string field = set[colIdx];
                string placeholder = ExtractPlaceholderFromField(field, players);
                
                row = colIdx == set.Length - 1 ? $@"{row}{placeholder} " : $@"{row}{placeholder} | ";
            }

            return row;
        }
        
        private static string PrintRowBorder(int dimension)
        {
            int times = (dimension - 2) * 4 + 2 * 3 + (dimension + 1);
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < times; i++)
            {
                sb.Append("-");
            }
            return sb.ToString();
        }
        
        private static string ExtractPlaceholderFromField(string proxy, Dictionary<string, Player> players)
        {
            return players.ContainsKey(proxy) ? 
                players[proxy].GetMarker() :
                proxy.Length < 2 ? $"0{proxy}" : 
                proxy;
        }
    }
}