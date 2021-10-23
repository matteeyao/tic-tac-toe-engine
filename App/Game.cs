using System;
using System.Collections.Generic;
using App.Players;
using App.UI;

namespace App
{
    public class Game
    {
        private Dictionary<string, Player> players;
        private Board board;
        private string turn;

        public Game(Player playerOne, Player playerTwo, Board.Dimensions boardSize)
        {
            this.players = new Dictionary<string, Player>()
            {
                {Board.Marks.x.ToString(), playerOne},
                {Board.Marks.o.ToString(), playerTwo}
            };
            this.board = new Board(boardSize);
            this.turn = Board.Marks.x.ToString();
        }

        public virtual Board GetBoard()
        {
            return this.board;
        }

        public virtual string GetTurn()
        {
            return this.turn;
        }

        public void PrintBoard()
        {
            MessageHandler.PrintBoard(board.GetGrid(), this.players);
        }

        public void Run()
        {
            while (!this.IsOver())
            {
                this.PlayTurn();
                this.SwapTurn();
            }
            this.PrintResults();
        }

        public void PlayTurn() 
        {
            Player currentPlayer = this.players[this.turn];
            int pos = currentPlayer.Move(this, this.turn);
            this.board.SetField(pos, this.turn);
        }

        public bool IsOver()
        {
            return this.board.HasWinner() || this.board.IsTied();
        }

        public void SwapTurn()
        {
            this.turn = ((this.turn.Equals(Board.Marks.x.ToString())) ? Board.Marks.o.ToString() : Board.Marks.x.ToString());
        }
        
        public void PrintResults()
        {
            this.PrintBoard();
            if (this.board.HasWinner())
            {
                Player WinningPlayer = this.players[this.board.Winner()];
                PrintWinner(WinningPlayer);
            }
            else
            {
                PrintDraw();
            }
        }

        public void PrintWinner(Player player)
        {
            MessageHandler.PrintDeclarationOfWinner(player.GetMarker());
        }

        public void PrintDraw()
        {
            MessageHandler.Print(MessageHandler.StaticMessage.DeclarationOfDraw);
        }
    }
}
