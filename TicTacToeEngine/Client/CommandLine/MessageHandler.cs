using System;
using TicTacToeEngine.UI.Message;

namespace TicTacToeEngine.Client.CLI
{
    public class MessageHandler : IClient.Interactable
    {
        private string message;
        public string Message
        {
            get => message;
            set => message = value;
        }
        private string input;
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
            Message = message.GetMessage();
            Console.Write(Message);
        }
        
        public void PrintError(IPrintable message)
        {
            Error = message.GetMessage();
            Console.Write(Error);
        }

        public void ClearError()
        {
            Error = String.Empty;
        }

        public string Read()
        {
            Input = Console.ReadLine();
            return Input;
        }
    }
}
