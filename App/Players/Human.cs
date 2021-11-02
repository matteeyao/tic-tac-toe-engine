using App.Client;
using App.Client.CLI;
using App.UI;

namespace App.Players
{
    public class Human : Player
    {
        public Human(string marker) : base(marker) { }

        public override int Move(IRunnable client, Game game, string turnDesignatedMark)
        {
            client.Board();
            return client.GetPrompt().GetMove(this.GetMarker(), game.GetBoard());
        }
    }
}
