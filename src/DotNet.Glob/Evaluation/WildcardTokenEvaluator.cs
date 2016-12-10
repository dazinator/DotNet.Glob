using DotNet.Globbing.Token;
using System;
using System.Text;

namespace DotNet.Globbing.Evaluation
{
    public class WildcardTokenEvaluator : IGlobTokenEvaluator
    {
        private readonly WildcardToken _token;
        //private readonly IGlobToken[] _subTokens;
        private readonly GlobTokenEvaluator _subEvaluator;

        public WildcardTokenEvaluator(WildcardToken token, GlobTokenEvaluator subEvaluator)
        {
            _token = token;
            _subEvaluator = subEvaluator;
        }

        #region IGlobTokenEvaluator

        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            // When called as an evaluator, we act as a wildcard evaluator - we only return true,
            // if all of our child evaluators return true.
            // but as we are a wildcard, we keep advancing a character in the current path segment and retrying
            // until we find a match or reach the end of the string.
            newPosition = currentPosition;
            int endOfSegmentPos;
            var remainingText = ReadRemaining(allChars, currentPosition, out endOfSegmentPos);

            //  var matchedText = new StringBuilder(endOfSegmentPos);

            for (int i = 0; i < endOfSegmentPos; i++)
            {
                var isMatch = _subEvaluator.IsMatch(remainingText);
                if (isMatch)
                {
                   // _subEvaluator._currentCharIndex
                    newPosition = allChars.Length - 1;
                    return true;
                }

                // match a single character
                //  matchedText.Append(remainingText[0]);
                // re-attempt matching of child tokens against this remaining string.
                newPosition = newPosition + 1;
                remainingText = remainingText.Substring(1);

            }

            return false;
        }

        #endregion

        public char ReadChar(string text, int currentPosition)
        {
            if (currentPosition >= text.Length)
            {
                return Char.MinValue;
            }
            var result = text[currentPosition];
            return result;
        }

        public string ReadRemaining(string text, int currentPosition, out int positionOfNextPathSeperator)
        {
           
            var builder = new StringBuilder(text.Length - currentPosition);
            var nextChar = ReadChar(text, currentPosition);
            positionOfNextPathSeperator = 0;
            positionOfNextPathSeperator = currentPosition;

            while (nextChar != Char.MinValue)
            {
                builder.Append(nextChar);
                if (nextChar == '/' || nextChar == '\\')
                {
                    break;
                }
                else
                {
                    positionOfNextPathSeperator = positionOfNextPathSeperator + 1;
                }
                currentPosition = currentPosition + 1;
                nextChar = ReadChar(text, currentPosition);
            }

            return builder.ToString();

        }

    }
}