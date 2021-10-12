using System;

namespace App.UI
{
    public class Prompt
    {
        public static int GetGameMode()
        {
            MessageHandler.Print(MessageHandler.StaticMessage.Greeting);
            MessageHandler.Print(MessageHandler.StaticMessage.GameModes);
            MessageHandler.Print(MessageHandler.StaticMessage.RequestToChooseGameMode);
            return GetInteger(MessageHandler.StaticMessage.RequestToChooseGameModeAfterInvalidInput,
                Validator.IsGameModeValid);
        }

        public static Board.Dimensions GetBoardSize()
        {
            MessageHandler.Print(MessageHandler.StaticMessage.RequestToInputBoardSize);
            int boardSize = GetInteger(MessageHandler.StaticMessage.RequestToInputBoardSizeAfterInvalidInput,
                Validator.IsBoardSizeValid);
            return (Board.Dimensions) Enum.ToObject(typeof(Board.Dimensions), boardSize);
        }
        
        public static string GetPlayerOneMarker(bool isOpponentComputer = true)
        {
            MessageHandler.Print(MessageHandler.DynamicMessage.RequestForPlayerOnesMarker(isOpponentComputer));
            string marker = GetString(MessageHandler.StaticMessage.NoticeForInvalidMarker,
                Validator.IsMarkerValid);
            return DefaultCrossEmojiMarkerGivenInputtedMarkerIsNull(marker);
        }

        private static string DefaultCrossEmojiMarkerGivenInputtedMarkerIsNull(string marker)
        {
            return marker.Length == 0 ? MessageHandler.DefaultBoardEmojiMarker.Cross.code : marker;
        }
        
        public static string GetPlayerTwoMarker()
        {
            MessageHandler.Print(MessageHandler.StaticMessage.RequestForPlayerTwosMarker);
            string marker = GetString(MessageHandler.StaticMessage.NoticeForInvalidMarker,
                Validator.IsMarkerValid);
            return DefaultCircleEmojiMarkerGivenInputtedMarkerIsNull(marker);
        }
        
        private static string DefaultCircleEmojiMarkerGivenInputtedMarkerIsNull(string marker)
        {
            return marker.Length == 0 ? MessageHandler.DefaultBoardEmojiMarker.Circle.code : marker;
        }

        public static int GetMove(string mark, Board board)
        {
            MessageHandler.PrintRequestForPlayerToInputMove(mark, board.GetDimension());
            string input = MessageHandler.ReadInput();
            
            if (!Validator.IsInputAPositiveInteger(input))
            {
                MessageHandler.Print(MessageHandler.StaticMessage.NoticeForInvalidPosition);
                return GetMove(mark, board);
            }
            
            int index = ConvertStringToIntegerOutput(input) - 1;
            
            if (!Validator.IsMoveWithinBounds(board, index))
            {
                MessageHandler.Print(MessageHandler.StaticMessage.NoticeForInvalidPosition);
                return GetMove(mark, board);
            }

            if (!Validator.IsMoveAvailable(board, index))
            {
                MessageHandler.Print(MessageHandler.StaticMessage.NoticeIfPositionIsTaken);
                return GetMove(mark, board);
            }

            return index;
        }

        private static string GetString(MessageHandler.StaticMessage message, Func<string, bool> validator)
        {
            string input = MessageHandler.ReadInput();
            
            if (validator(input))
            {
                return input;
            }

            MessageHandler.Print(message);
            return GetString(message, validator);
        }

        private static int GetInteger(MessageHandler.StaticMessage message, Func<string, bool> validator)
        {
            string input = MessageHandler.ReadInput();
            
            if (validator(input))
            {
                return ConvertStringToIntegerOutput(input);
            }

            MessageHandler.Print(message);
            return GetInteger(message, validator);
        }
        
        private static int ConvertStringToIntegerOutput(string input)
        {
            return Int32.Parse(input);
        }
    }
}
