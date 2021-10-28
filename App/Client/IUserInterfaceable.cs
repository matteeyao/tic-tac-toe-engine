using App.UI;
using App.UI.Message;

namespace App.Client
{
    public interface IUserInterfaceable
    {
        public interface Interactable
        {
            public string Print(IPrintable message);
            public string Read(string input = null);
        }

        public Interactable GetMessageHandler();
        public Prompt GetPrompt();
        public void Run(IUserInterfaceable client);
        public string[] Board();
    }
}
