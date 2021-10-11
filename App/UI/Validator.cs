using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.UI
{
    public class Validator
    {
        private static int ParseIntFromInput(string input)
        {
            return Int32.TryParse(input, out int num) ? num : -1;
        }
        
        public static bool IsGameModeValid(string input)
        {
            int option = ParseIntFromInput(input);
            // return Enumerable.Range(1, 3).Contains(option);
            return Enumerable.Range(1, 1).Contains(option);
        }

        public static bool IsBoardSizeValid(string input)
        {
            int option = ParseIntFromInput(input);
            return Enum.IsDefined(typeof(Board.Dimensions), option);
        }
        
        public static bool IsMarkerValid(string text)
        {
            return IsMarkerAnEmoji(text);
        }
        
        private static bool IsMarkerAnEmoji(string text)
        {
            var emojiPattern = EmojiValidator.EmojiPattern;
            return Regex.Match(text, emojiPattern, RegexOptions.IgnoreCase).Success;
        }

        public static bool IsInputAPositiveInteger(string input)
        {
            return 0 <= ParseIntFromInput(input);
        }

        public static bool IsMoveWithinBounds(Board board, int index)
        {
            return board.IsValidField(index);
        }

        public static bool IsMoveAvailable(Board board, int index)
        {
            return board.IsEmptyField(index);
        }
    }
}
