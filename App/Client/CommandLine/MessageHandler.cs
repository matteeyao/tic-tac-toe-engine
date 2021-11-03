using System;
using App.UI.Message;

namespace App.Client.CLI
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
            
        public void Print(IPrintable message)
        {
            Message = message.GetMessage();
            Console.Write(Message);
        }
    
        public string Read()
        {
            Input = Console.ReadLine();
            return Input;
        }
    }
}
