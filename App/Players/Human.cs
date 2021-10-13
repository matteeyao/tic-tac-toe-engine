using App.UI;

namespace App.Players
{
    public class Human : Player
    {
        public Human(string marker) : base(marker) { }

        public override int Move(Game game, string turnDesignatedMark)
        {
            game.PrintBoard();
            return Prompt.GetMove(this.GetMarker(), game.GetBoard());
        }
    }
}
