namespace TicTacToeEngine.UI.Message
{
    public class DefaultBoardEmojiMarker
    {
        public string code { get; private set; }

        private DefaultBoardEmojiMarker(string code) { this.code = code; }
            
        public static DefaultBoardEmojiMarker Cross => new DefaultBoardEmojiMarker("\u274C");
        public static DefaultBoardEmojiMarker Circle => new DefaultBoardEmojiMarker("\u2B55");
    }
}
