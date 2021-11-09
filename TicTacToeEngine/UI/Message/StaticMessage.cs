namespace TicTacToeEngine.UI.Message
{
    public class StaticMessage : IPrintable
    {
        private readonly string value;

        private StaticMessage(string value)
        {
            this.value = value;
        }

        public string GetMessage()
        {
            return this.value;
        }

        public static StaticMessage Greeting => new StaticMessage("Welcome to Tic-Tac-Toe!\n");
        // public static StaticMessage GameModes => new StaticMessage("(1) Play against a friend\n(2) Play against an easy competitor\n(3) Play against a super computer\n\n");
        public static StaticMessage GameModes => new StaticMessage("(1) Play against a friend\n\n");
        // public static StaticMessage RequestToChooseGameModeAfterInvalidInput => new StaticMessage("Invalid option. Choose again from options 1-3: ");
        public static StaticMessage RequestToChooseGameMode => new StaticMessage("Choose from one of the above options: ");
        public static StaticMessage RequestToChooseGameModeAfterInvalidInput => new StaticMessage("Invalid option. Choose option 1: ");
        public static StaticMessage RequestToInputBoardSize => new StaticMessage("Enter board size 3, 4, or 5 (Press enter to default to 3): ");
        public static StaticMessage RequestToInputBoardSizeAfterInvalidInput => new StaticMessage("Invalid board size. Enter board size 3, 4, or 5: ");
        public static StaticMessage RequestForPlayerTwosMarker => new StaticMessage($"Enter player two's emoji mark (Hit enter to default to {DefaultBoardEmojiMarker.Circle.code}): ");
        public static StaticMessage NoticeForInvalidMarker => new StaticMessage("Invalid emoji mark!\n");
        public static StaticMessage NoticeForInvalidPosition => new StaticMessage("Invalid position! ");
        public static StaticMessage NoticeIfPositionIsTaken => new StaticMessage("Position is already taken!\n");
        public static StaticMessage DeclarationOfDraw => new StaticMessage("No one wins!\n");
    }
}
