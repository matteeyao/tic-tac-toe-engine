using System.Linq;
using App.Client.CLI;
using App.Players;
using App.UI;
using App.UI.Message;

namespace App.Client.Web
{
    public class WebInterface : IUserInterfaceable
    {
        public class MessageHandler : IUserInterfaceable.Interactable
        {
            public string currentMessage { get; set; }
            public string currentInput { get; set; }
            
            public void Print(IPrintable message)
            {
                this.currentMessage = message.GetMessage();
            }
        
            public string Read(string input)
            {
                this.currentInput = input;
                return this.currentInput;
            }
        }
        
        private static MessageHandler messageHandler;
        private static Prompt prompt;
        private Game game;
        
        public WebInterface(Game game = null)
        {
            messageHandler = new MessageHandler();
            prompt  = new Prompt(messageHandler);
            this.game = game ?? SetupGame();
        }
        
        public IUserInterfaceable.Interactable GetMessageHandler()
        {
            return messageHandler;
        }

        public Prompt GetPrompt()
        {
            return prompt;
        }

        public string GetMessage()
        {
            return messageHandler.currentMessage;
        }

        public void Run(IUserInterfaceable client)
        {
            game.Run(client);
        }

        public string[] Board()
        {
            return game.GetBoard().GetGrid().Cast<string>().ToArray();
        }
        
        private Game SetupGame()
        {
            return SetupCustomGame();
        }
        
        private Game SetupCustomGame()
        {
            return new Game(
                new Human(DefaultBoardEmojiMarker.Cross.code),
                new Human(DefaultBoardEmojiMarker.Circle.code),
                App.Board.Dimensions.ThreeByThree);
        }
    }
}
