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
            public string Print(IPrintable message)
            {
                return message.GetMessage();
            }
        
            public string Read(string input)
            {
                return input;
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
