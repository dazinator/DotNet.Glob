using System;
using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    internal class WildcardDirectoryTokenEvaluator : IGlobTokenEvaluator
    {
       // private IGlobToken[] _remaining;
        private WildcardDirectoryToken _token;
        private readonly GlobTokenEvaluator _subEvaluator;

        public WildcardDirectoryTokenEvaluator(WildcardDirectoryToken token, GlobTokenEvaluator subEvaluator)
        {
            _token = token;
            //_remaining = remaining;
            _subEvaluator = subEvaluator;
        }

        #region IGlobTokenEvaluator

        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            // When called as an evaluator, we act as a directory wildcard evaluator - we only return true,
            // if all of our child evaluators return true.
            // but as we are a wildcard, if the children evaluators dont immediately match, we can 
            // avance a character in the current path segment and retry.
            // However because we are also a directory wildcard, we don't have to stop at the end of the current
            // path segment, we can keep going until the end of the string.
            // TODO: we could probably implement some better heuristics to fail early.
            // for example, if we know the minimum number or characters that must be remaining in the string in 
            // order to have a chance of matching all the available tokens, then we can fail / stop attemtping to match
            // if we haven't found a match and have reached that limit.

            newPosition = currentPosition;
           // int endOfSegmentPos
            var remainingText = allChars.Substring(currentPosition); // ReadRemaining(allChars, currentPosition, out endOfSegmentPos);
            int haltAtPosition = remainingText.Length; // TODO: calculate this better as per above comments.
            //  var matchedText = new StringBuilder(endOfSegmentPos);

            for (int i = 0; i < haltAtPosition; i++)
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

        //public char ReadChar(string text, int currentPosition)
        //{
        //    if (currentPosition >= text.Length)
        //    {
        //        return Char.MinValue;
        //    }
        //    var result = text[currentPosition];
        //    return result;
        //}

        //public string ReadRemaining(string text, int currentPosition, out int positionOfNextPathSeperator)
        //{

        //    var builder = new StringBuilder(text.Length - currentPosition);
        //    var nextChar = ReadChar(text, currentPosition);
        //    positionOfNextPathSeperator = 0;
        //    positionOfNextPathSeperator = currentPosition;

        //    while (nextChar != Char.MinValue)
        //    {
        //        builder.Append(nextChar);
        //        if (nextChar == '/' || nextChar == '\\')
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            positionOfNextPathSeperator = positionOfNextPathSeperator + 1;
        //        }
        //        currentPosition = currentPosition + 1;
        //        nextChar = ReadChar(text, currentPosition);
        //    }

        //    return builder.ToString();

        //}
    }
}