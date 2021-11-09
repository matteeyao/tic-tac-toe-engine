using TicTacToeEngine.Client;
using TicTacToeEngine.Client.CLI;

namespace TicTacToeEngine
{
    public class TicTacToe
    {
        private readonly IClient client;

        public TicTacToe(IClient client = null)
        {
            this.client = client ?? new CommandLine();
        }

        public void Run()
        {
            client.Run(client);
        }
    }
}
