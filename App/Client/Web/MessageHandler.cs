using System;
using App.UI.Message;

namespace App.Client.Web
{
    public static class MessageHandler
    {
        public static string Print(IPrintable message)
        {
            return message.GetMessage();
        }
        
        public static string Read(string input)
        {
            return input;
        }
    }
}
