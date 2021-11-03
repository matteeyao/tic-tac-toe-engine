using App.Client;

namespace App.Players
{
    public abstract class Player
    {
        private string marker;

        protected Player(string marker)
        {
            this.marker = marker;
        }

        public virtual string GetMarker()
        {
            return this.marker;
        }

        public abstract int Move(IClient interfaceable, Game game, string mark, string input);
    }
}
