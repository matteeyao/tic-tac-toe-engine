using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeEngine.Players;

namespace TicTacToeEngine.UI.Message
{
    public class DynamicMessage : IPrintable
    {
        private readonly string value;

        private DynamicMessage(string value)
        {
            this.value = value;
        }

        public string GetMessage()
        {
            return this.value;
        }

        public static DynamicMessage RequestForPlayerOnesMarker(bool isOpponentComputer)
        {
            string title = isOpponentComputer ? "your" : "player one's";
            return new DynamicMessage($"Enter {title} emoji mark (Hit enter to default to {DefaultBoardEmojiMarker.Cross.code}): ");
        }

        public static DynamicMessage RequestForPlayerToInputMove(string mark, int boardDimension)
        {
            return new DynamicMessage($"{mark} enter a position 1-{(int) Math.Pow(boardDimension, 2)} to mark: ");
        }

        public static DynamicMessage DeclarationOfWinner(string mark)
        {
            return new DynamicMessage($"{mark} won the game!");
        }

        public static DynamicMessage Board(string[,] rows, Dictionary<string, Player> players)
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
                    string.Format("{0}\n{1}\n\n", board, PrintRow(row, players)) : 
                    string.Format("{0}\n{1}\n{2}", board, PrintRow(row, players), PrintRowBorder(rowLength));
            }

            return new DynamicMessage(board);
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
