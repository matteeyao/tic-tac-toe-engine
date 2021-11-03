using System.Threading.Tasks;
using App.UI;
using App.UI.Message;

namespace App.Client
{
    public interface IClient
    {
        public interface Interactable
        {
            public string Message { get; set; }
            public string Input { get; set; }
            public void Print(IPrintable message);
            public string Read();
        }
        
        public Interactable MessageHandler { get; }
        public Prompt Prompt { get; }
        public void Run(IClient client, string input = null);
        public int GetMove(string marker, string input);
        public string[] Board();
    }
}
