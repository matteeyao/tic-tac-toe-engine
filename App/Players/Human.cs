using App.Client;
using App.Client.CLI;
using App.UI;

namespace App.Players
{
    public class Human : Player
    {
        public Human(string marker) : base(marker) { }

        public override int Move(IClient client, Game game, string turnDesignatedMark)
        {
            client.Board();
            return Prompt.GetMove(this.GetMarker(), game.GetBoard());
        }
    }
}
