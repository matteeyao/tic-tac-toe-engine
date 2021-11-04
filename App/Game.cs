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

        public void PrintBoard(IClient.Interactable messageHandler)
        {
            messageHandler.Print(DynamicMessage.Board(board.GetGrid(), players));
        }

        public string FetchMarker(string sequence)
        {
            return players.ContainsKey(sequence) ? players[sequence].GetMarker() : sequence;
        }

        public virtual void Run(IClient client)
        {
            while (!IsOver())
            {
                Play(client);
            }
            PrintResults(client);
        }
        
        public virtual void InvokeTurn(IClient client, string input)
        {
            Play(client, input);

            if (IsOver())
            {
                PrintResults(client);
            }
            else
            {
                PromptMoveMessage(client);
            }
        }
        
        private void Play(IClient client, string input = null)
        {
            PlayTurn(client, input);
            SwapTurn();
        }
        
        private bool IsOver()
        {
            return this.board.HasWinner() || this.board.IsTied();
        }
        
        private void PlayTurn(IClient client, string input)
        {
            Player currentPlayer = players[turn];
            int pos = currentPlayer.Move(client, this, this.turn, input);
            this.board.SetField(pos, this.turn);
        }
        
        private void SwapTurn()
        {
            this.turn = ((this.turn.Equals(Board.Marks.x.ToString())) ? Board.Marks.o.ToString() : Board.Marks.x.ToString());
        }

        public virtual void PromptMoveMessage(IClient client)
        {
            client.MessageHandler.Print(
                DynamicMessage.RequestForPlayerToInputMove(players[turn].GetMarker(), board.GetDimension())
            );
        }
        
        private void PrintResults(IClient client)
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

        private void PrintWinner(IClient client, Player player)
        {
            client.MessageHandler.Print(DynamicMessage.DeclarationOfWinner(player.GetMarker()));
        }

        private void PrintDraw(IClient client)
        {
            client.MessageHandler.Print(StaticMessage.DeclarationOfDraw);
        }
    }
}
