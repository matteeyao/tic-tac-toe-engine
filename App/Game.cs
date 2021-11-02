using System;
using System.Collections.Generic;
using App.Client;
using App.Client.CLI;
using App.Players;
using App.UI;
using App.UI.Message;

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

        public void PrintBoard(IRunnable.Interactable messageHandler)
        {
            messageHandler.Print(DynamicMessage.Board(board.GetGrid(), players));
        }

        public virtual void Run(IRunnable client)
        {
            while (!this.IsOver())
            {
                this.PlayTurn(client);
                this.SwapTurn();
            }
            this.PrintResults(client);
        }

        private void PlayTurn(IRunnable client)
        {
            Player currentPlayer = this.players[this.turn];
            int pos = currentPlayer.Move(client, this, this.turn);
            this.board.SetField(pos, this.turn);
        }

        private bool IsOver()
        {
            return this.board.HasWinner() || this.board.IsTied();
        }

        private void SwapTurn()
        {
            this.turn = ((this.turn.Equals(Board.Marks.x.ToString())) ? Board.Marks.o.ToString() : Board.Marks.x.ToString());
        }
        
        private void PrintResults(IRunnable client)
        {
            client.Board();
            if (this.board.HasWinner())
            {
                Player winningPlayer = this.players[this.board.Winner()];
                PrintWinner(client, winningPlayer);
            }
            else
            {
                PrintDraw(client);
            }
        }

        private void PrintWinner(IRunnable client, Player player)
        {
            client.GetMessageHandler().Print(DynamicMessage.DeclarationOfWinner(player.GetMarker()));
        }

        private void PrintDraw(IRunnable client)
        {
            client.GetMessageHandler().Print(StaticMessage.DeclarationOfDraw);
        }
    }
}
