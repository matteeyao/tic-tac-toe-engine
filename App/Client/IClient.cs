namespace App.Client
{
    public interface IClient
    {
        public void Run(IClient client);
        public string[] Board();
    }
}
