using App.Client;
using App.Client.CLI;

namespace App
{
    public class TicTacToe
    {
        private readonly IClient client;

        public TicTacToe(IClient client = null)
        {
            this.client = client ?? new CommandLineInterface();
        }

        public void Run()
        {
            client.Run(client);
        }
    }
}
