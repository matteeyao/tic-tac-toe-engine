using System;
using App.Client.CLI;
using App.UI.Message;

namespace App.UI
{
    public class Prompt
    {
        public static int GetGameMode()
        {
            MessageHandler.Print(StaticMessage.Greeting);
            MessageHandler.Print(StaticMessage.GameModes);
            MessageHandler.Print(StaticMessage.RequestToChooseGameMode);
            return GetInteger(StaticMessage.RequestToChooseGameModeAfterInvalidInput,
                Validator.IsGameModeValid);
        }

        public static Board.Dimensions GetBoardSize()
        {
            MessageHandler.Print(StaticMessage.RequestToInputBoardSize);
            int boardSize = GetInteger(StaticMessage.RequestToInputBoardSizeAfterInvalidInput,
                Validator.IsBoardSizeValid);
            return DefaultToThreeByThreeBoardSizeGivenInputtedDimensionIsNull(boardSize);
        }
        
        private static Board.Dimensions DefaultToThreeByThreeBoardSizeGivenInputtedDimensionIsNull(int boardSize)
        {
            return boardSize == -1 ? Board.Dimensions.ThreeByThree :
                (Board.Dimensions) Enum.ToObject(typeof(Board.Dimensions), boardSize);
        }
        
        public static string GetPlayerOneMarker(bool isOpponentComputer = true)
        {
            MessageHandler.Print(DynamicMessage.RequestForPlayerOnesMarker(isOpponentComputer));
            string marker = GetString(StaticMessage.NoticeForInvalidMarker,
                Validator.IsMarkerValid);
            return DefaultCrossEmojiMarkerGivenInputtedMarkerIsNull(marker);
        }

        private static string DefaultCrossEmojiMarkerGivenInputtedMarkerIsNull(string marker)
        {
            return marker.Length == 0 ? DefaultBoardEmojiMarker.Cross.code : marker;
        }
        
        public static string GetPlayerTwoMarker()
        {
            MessageHandler.Print(StaticMessage.RequestForPlayerTwosMarker);
            string marker = GetString(StaticMessage.NoticeForInvalidMarker,
                Validator.IsMarkerValid);
            return DefaultCircleEmojiMarkerGivenInputtedMarkerIsNull(marker);
        }
        
        private static string DefaultCircleEmojiMarkerGivenInputtedMarkerIsNull(string marker)
        {
            return marker.Length == 0 ? DefaultBoardEmojiMarker.Circle.code : marker;
        }

        public static int GetMove(string mark, Board board)
        {
            MessageHandler.Print(DynamicMessage.RequestForPlayerToInputMove(mark, board.GetDimension()));
            string input = MessageHandler.Read();
            
            if (!Validator.IsInputAPositiveInteger(input))
            {
                MessageHandler.Print(StaticMessage.NoticeForInvalidPosition);
                return GetMove(mark, board);
            }
            
            int index = ConvertStringToIntegerOutput(input) - 1;
            
            if (!Validator.IsMoveWithinBounds(board, index))
            {
                MessageHandler.Print(StaticMessage.NoticeForInvalidPosition);
                return GetMove(mark, board);
            }

            if (!Validator.IsMoveAvailable(board, index))
            {
                MessageHandler.Print(StaticMessage.NoticeIfPositionIsTaken);
                return GetMove(mark, board);
            }

            return index;
        }

        private static string GetString(StaticMessage message, Func<string, bool> validator)
        {
            string input = MessageHandler.Read();
            
            if (validator(input))
            {
                return input;
            }

            MessageHandler.Print(message);
            return GetString(message, validator);
        }

        private static int GetInteger(StaticMessage message, Func<string, bool> validator)
        {
            string input = MessageHandler.Read();
            
            if (validator(input))
            {
                return ConvertStringToIntegerOutput(input);
            }

            MessageHandler.Print(message);
            return GetInteger(message, validator);
        }
        
        private static int ConvertStringToIntegerOutput(string input)
        {
            return Int32.TryParse(input, out int result) ? result : -1;
        }
    }
}
