using System;
using System.Linq;
using System.Threading.Tasks;
using App.Players;
using App.UI;
using App.UI.Message;

namespace App.Client.Web
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

        public Prompt GetPrompt()
        {
            return prompt;
        }

        public string GetMessage()
        {
            return messageHandler.Message;
        }

        public string[] Board()
        {
            return game
                .GetBoard()
                .GetGrid()
                .Cast<string>()
                .ToArray();
        }
        
        private Game SetupGame()
        {
            Board.Dimensions boardSize = App.Board.Dimensions.ThreeByThree;
            string playerOneMarker = DefaultBoardEmojiMarker.Cross.code;
            string playerTwoMarker = DefaultBoardEmojiMarker.Circle.code;
            return SetupCustomGame(playerOneMarker, playerTwoMarker, boardSize);
        }

        public void Run(IClient client, string input)
        {
            game.InvokeTurn(client, input);
            game.PromptMoveMessage(client);
        }

        // TODO: TEST
        public int GetMove(string marker, string input)
        {
            Board();
            if (!Prompt.IsInputMoveValid(game.GetBoard(), input))
            {
                return -1;
            }
            return Prompt.GetValidMove(input);
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
