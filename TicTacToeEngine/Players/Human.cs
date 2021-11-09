using TicTacToeEngine.Client;

namespace TicTacToeEngine.Players
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
