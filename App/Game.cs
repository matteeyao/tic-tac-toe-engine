using System;
using System.Collections.Generic;
using App.Players;
using App.UI;

namespace App
{
    public sealed class Game
    {
        private Dictionary<Board.Marks, Player> players;
        private Board board;
        private Board.Marks turn;

        public Game(Player playerOne, Player playerTwo, Board.Dimensions boardSize)
        {
            this.players = new Dictionary<Board.Marks, Player>()
            {
                {Board.Marks.x, playerOne},
                {Board.Marks.o, playerTwo}
            };
            this.board = new Board(boardSize);
            this.turn = Board.Marks.x;
        }

        public Board GetBoard()
        {
            return this.board;
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

        private void PlayTurn()
        {
            Player currentPlayer = this.players[this.turn];
            int pos = currentPlayer.Move(this, this.turn);
            this.board.SetField(pos, this.turn);
        }

        private bool IsOver()
        {
            return this.board.HasWinner() || this.board.IsTied();
        }

        private void SwapTurn()
        {
            // swap next whose turn it will be next
            this.turn = ((this.turn.Equals(Board.Marks.x)) ? Board.Marks.o : Board.Marks.x);
        }

        private void PrintResults()
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

        private void PrintWinner(Player player)
        {
            MessageHandler.PrintDeclarationOfWinner(player.GetMarker());
        }

        private void PrintDraw()
        {
            MessageHandler.Print(MessageHandler.StaticMessage.DeclarationOfDraw);
        }
    }
}
