using System;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeEngine.Players;
using TicTacToeEngine.UI;
using TicTacToeEngine.UI.Message;

namespace TicTacToeEngine.Client.Web
{
    public class Web : IClient
    {
        private static IClient.Interactable messageHandler;
        public IClient.Interactable MessageHandler => messageHandler;
        private static Prompt prompt;
        public Prompt Prompt => prompt;
        private Game game;
        
        public Web(Game game = null)
        {
            messageHandler = new MessageHandler();
            prompt = new Prompt(messageHandler);
            this.game = game ?? SetupGame();
        }
        
        public IClient.Interactable GetMessageHandler()
        {
            return messageHandler;
        }

        public string[] Board()
        {
            return game
                .GetBoard()
                .GetGrid()
                .Cast<string>()
                .Select(s => game.FetchMarker(s))
                .ToArray();
        }
        
        public void Run(IClient client, string input)
        {
            if (!Prompt.IsInputMoveValid(game.GetBoard(), input)) return;
            game.InvokeTurn(client, input);
        }
        
        public int GetMove(string marker, string input)
        {
            return Prompt.GetValidMove(input);
        }

        public bool IsGameOver()
        {
            return game.IsOver();
        }
        
        private Game SetupGame()
        {
            Board.Dimensions boardSize = TicTacToeEngine.Board.Dimensions.ThreeByThree;
            string playerOneMarker = DefaultBoardEmojiMarker.Cross.code;
            string playerTwoMarker = DefaultBoardEmojiMarker.Circle.code;
            return SetupCustomGame(playerOneMarker, playerTwoMarker, boardSize);
        }

        private Game SetupCustomGame(string playerOneMarker, string playerTwoMarker, Board.Dimensions boardSize)
        {
            Game game = new Game(
                new Human(playerOneMarker),
                new Human(playerTwoMarker),
                boardSize);
            game.PromptMoveMessage(this);
            return game;
        }
    }
}
