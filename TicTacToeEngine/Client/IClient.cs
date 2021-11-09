using System.Threading.Tasks;
using TicTacToeEngine.UI;
using TicTacToeEngine.UI.Message;

namespace TicTacToeEngine.Client
{
    public interface IClient
    {
        public interface Interactable
        {
            public string Message { get; set; }
            public string Input { get; set; }
            public string Error { get; set; }
            public void Print(IPrintable message);
            public void PrintError(IPrintable message);
            public void ClearError();
            public string Read();
        }
        
        public Interactable MessageHandler { get; }
        public Prompt Prompt { get; }
        public void Run(IClient client, string input = null);
        public int GetMove(string marker, string input);
        public string[] Board();
    }
}
