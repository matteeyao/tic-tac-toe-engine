using App.Client;
using App.Client.CLI;

namespace App
{
    public class TicTacToe
    {
        private readonly IRunnable client;

        public TicTacToe(IRunnable client = null)
        {
            this.client = client ?? new CommandLine();
        }

        public void Run()
        {
            client.Run(client);
        }
    }
}
