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
            this.game.Run(client);
        }

        public string[] Board()
        {
            return new string[9];
        }
    }
}
