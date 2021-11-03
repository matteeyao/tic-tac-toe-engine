using App.Client;

namespace App.Players
{
    public class Human : Player
    {
        public Human(string marker) : base(marker) { }

        public override int Move(IClient client, Game game, string turnDesignatedMark, string input)
        {
            return client.GetMove(this.GetMarker(), input);
        }
    }
}
