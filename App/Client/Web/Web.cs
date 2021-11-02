using System;
using System.Linq;
using App.Players;
using App.UI;
using App.UI.Message;

namespace App.Client.Web
{
    public class Web : IRunnable
    {
        public class MessageHandler : IRunnable.Interactable
        {
            private string message = String.Empty;
            public string Message
            {
                get => message;
                set => message = value;
            }
            
            private string input = String.Empty;
            public string Input
            {
                get => input;
                set
                {
                    input = value;
                    isInputChanged = true;
                }
            }

            private bool isInputChanged;
            public bool IsInputChanged
            {
                get => isInputChanged;
                set => isInputChanged = value;
            }
            
            public void Print(IPrintable message)
            {
                this.Message = message.GetMessage();
            }
        
            public string Read()
            {
                if (!IsInputChanged) return Read();
                IsInputChanged = false;
                return Input;
            }
        }
        
        private static MessageHandler messageHandler;
        private static Prompt prompt;
        private Game game;
        
        public Web(Game game = null)
        {
            messageHandler = new MessageHandler();
            prompt  = new Prompt(messageHandler);
            this.game = game ?? SetupGame();
        }
        
        public IRunnable.Interactable GetMessageHandler()
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
        
        public void Run(IRunnable client)
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
