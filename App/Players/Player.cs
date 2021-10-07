namespace App.Players
{
    public abstract class Player
    {
        private string marker;

        public virtual string GetMarker()
        {
            return this.marker;
        }
    }
}
