using System;
using TicTacToeEngine.UI.Message;

namespace TicTacToeEngine.Client.Web
{
    public class MessageHandler : IClient.Interactable
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
            set => input = value;
        }
        private string error;
        public string Error
        {
            get => error;
            set => error = value;
        }
        
        public void Print(IPrintable message)
        {
            this.Message = message.GetMessage();
        }
        
        public void PrintError(IPrintable message)
        {
            Error = message.GetMessage();
        }
        
        public void ClearError()
        {
            Error = String.Empty;
        }
        
        public string Read()
        {
            return Input;
        }
    }
}
