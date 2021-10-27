using System;
using System.Threading.Tasks;
using App.UI.Message;

namespace App.Client.CLI
{
    public static class MessageHandler
    {
        public static void Print(IPrintable message)
        {
            Console.Write(message.GetMessage());
        }

        public static string Read()
        {
            return Console.ReadLine();
        }
    }
}
