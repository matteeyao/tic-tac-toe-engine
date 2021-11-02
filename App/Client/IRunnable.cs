using System.Threading.Tasks;
using App.UI;
using App.UI.Message;

namespace App.Client
{
    public interface IRunnable
    {
        public interface Interactable
        {
            public string Message { get; set; }
            public string Input { get; set; }
            public void Print(IPrintable message);
            public string Read();
        }

        public Interactable GetMessageHandler();
        public Prompt GetPrompt();
        public void Run(IRunnable client);
        public string[] Board();
    }
}
