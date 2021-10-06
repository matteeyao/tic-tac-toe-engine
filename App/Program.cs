using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    class Program
    {
        class Player
        {
            private string marker;

            public Player(string marker)
            {
                this.marker = marker;
            }

            public string GetMarker()
            {
                return this.marker;
            }
        }
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            
            string[,] rows =
            {
                {"1", "2", "3"}, 
                {"4", "5", "6"},
                {"7", "8", "9"}
            };

            Player playerOne = new Player("\u274C");
            Player playerTwo = new Player("\u2B55");
            Dictionary<string, Player> players = new Dictionary<string, Player>(){
                {"x", playerOne},
                {"o", playerTwo}
            };

            Console.WriteLine(PrintBoard(rows, players));
            
            string[,] row1 =
            {
                {"x", "x", "x"}, 
                {"o", "o", "x"},
                {"o", "x", "x"}
            };
            
            Console.WriteLine(PrintBoard(row1, players));
            
            string[,] row2 =
            {
                {"1", "2", "3", "4"}, 
                {"5", "6", "7", "8"}, 
                {"9", "10", "11", "12"},
                {"13", "14", "15", "16"}, 
            };
            
            Console.WriteLine(PrintBoard(row2, players));
            
            string[,] row3 =
            {
                {"x", "x", "x", "o"}, 
                {"o", "o", "x", "x"},
                {"o", "x", "x", "o"},
                {"x", "x", "x", "x"}, 
            };
            
            Console.WriteLine(PrintBoard(row3, players));
        }
    
        private static string PrintBoard(string[,] rows, Dictionary<string, Player> players)
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
            return board;
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