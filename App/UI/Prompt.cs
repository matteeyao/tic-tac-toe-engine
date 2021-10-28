using System;
using App.Client;
using App.Client.CLI;
using App.UI.Message;

namespace App.UI
{
    public class Prompt
    {
        private IUserInterfaceable.Interactable messageHandler;

        public Prompt(IUserInterfaceable.Interactable messageHandler)
        {
            this.messageHandler = messageHandler;
        }
        
        public int GetGameMode()
        {
            messageHandler.Print(StaticMessage.Greeting);
            messageHandler.Print(StaticMessage.GameModes);
            messageHandler.Print(StaticMessage.RequestToChooseGameMode);
            return GetInteger(StaticMessage.RequestToChooseGameModeAfterInvalidInput,
                Validator.IsGameModeValid);
        }

        public Board.Dimensions GetBoardSize()
        {
            messageHandler.Print(StaticMessage.RequestToInputBoardSize);
            int boardSize = GetInteger(StaticMessage.RequestToInputBoardSizeAfterInvalidInput,
                Validator.IsBoardSizeValid);
            return DefaultToThreeByThreeBoardSizeGivenInputtedDimensionIsNull(boardSize);
        }
        
        private Board.Dimensions DefaultToThreeByThreeBoardSizeGivenInputtedDimensionIsNull(int boardSize)
        {
            return boardSize == -1 ? Board.Dimensions.ThreeByThree :
                (Board.Dimensions) Enum.ToObject(typeof(Board.Dimensions), boardSize);
        }
        
        public string GetPlayerOneMarker(bool isOpponentComputer = true)
        {
            messageHandler.Print(DynamicMessage.RequestForPlayerOnesMarker(isOpponentComputer));
            string marker = GetString(StaticMessage.NoticeForInvalidMarker,
                Validator.IsMarkerValid);
            return DefaultCrossEmojiMarkerGivenInputtedMarkerIsNull(marker);
        }

        private string DefaultCrossEmojiMarkerGivenInputtedMarkerIsNull(string marker)
        {
            return marker.Length == 0 ? DefaultBoardEmojiMarker.Cross.code : marker;
        }
        
        public string GetPlayerTwoMarker()
        {
            messageHandler.Print(StaticMessage.RequestForPlayerTwosMarker);
            string marker = GetString(StaticMessage.NoticeForInvalidMarker,
                Validator.IsMarkerValid);
            return DefaultCircleEmojiMarkerGivenInputtedMarkerIsNull(marker);
        }
        
        private string DefaultCircleEmojiMarkerGivenInputtedMarkerIsNull(string marker)
        {
            return marker.Length == 0 ? DefaultBoardEmojiMarker.Circle.code : marker;
        }

        public int GetMove(string mark, Board board)
        {
            messageHandler.Print(DynamicMessage.RequestForPlayerToInputMove(mark, board.GetDimension()));
            string input = messageHandler.Read();
            
            if (!Validator.IsInputAPositiveInteger(input))
            {
                messageHandler.Print(StaticMessage.NoticeForInvalidPosition);
                return GetMove(mark, board);
            }
            
            int index = ConvertStringToIntegerOutput(input) - 1;
            
            if (!Validator.IsMoveWithinBounds(board, index))
            {
                messageHandler.Print(StaticMessage.NoticeForInvalidPosition);
                return GetMove(mark, board);
            }

            if (!Validator.IsMoveAvailable(board, index))
            {
                messageHandler.Print(StaticMessage.NoticeIfPositionIsTaken);
                return GetMove(mark, board);
            }

            return index;
        }

        private string GetString(StaticMessage message, Func<string, bool> validator)
        {
            string input = messageHandler.Read();
            
            if (validator(input))
            {
                return input;
            }

            messageHandler.Print(message);
            return GetString(message, validator);
        }

        private int GetInteger(StaticMessage message, Func<string, bool> validator)
        {
            string input = messageHandler.Read();
            
            if (validator(input))
            {
                return ConvertStringToIntegerOutput(input);
            }

            messageHandler.Print(message);
            return GetInteger(message, validator);
        }
        
        private int ConvertStringToIntegerOutput(string input)
        {
            return Int32.TryParse(input, out int result) ? result : -1;
        }
    }
}
