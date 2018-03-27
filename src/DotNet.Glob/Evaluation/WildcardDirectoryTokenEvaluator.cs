using System;
using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    internal class WildcardDirectoryTokenEvaluator : IGlobTokenEvaluator
    {
        // private IGlobToken[] _remaining;
        private WildcardDirectoryToken _token;
        private readonly CompositeTokenEvaluator _subEvaluator;

        public WildcardDirectoryTokenEvaluator(WildcardDirectoryToken token, CompositeTokenEvaluator subEvaluator)
        {
            _token = token;
            //_remaining = remaining;
            _subEvaluator = subEvaluator;
        }

        #region IGlobTokenEvaluator

        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            // We shortcut to success for a ** in some special cases:-
            //  1. We are already at the end of the test string.
            //  2. The ** token is the last token - in which case it will math all remaining text

            // We shortcut to failure for a ** in some special cases:-
            // A) The token was parsed with a leading seperator (i.e '/**' and the current charater we are matching from doesn't match that seperator.

            newPosition = currentPosition;

            // 1. Are we already at the end of the test string?
            if (currentPosition >= allChars.Length)
            {
                return true;
            }

            // A) If leading seperator then current character needs to be that seperator.
            var currentChar = allChars[currentPosition];
            if (this._token.LeadingPathSeperator != null)
            {
                if (currentChar != this._token.LeadingPathSeperator.Value)
                {
                    // expected seperator.
                    return false;
                }
                // advance current position to match the leading seperator.
                currentPosition = currentPosition + 1;
            }

            // 2. if no more tokens require matching (i.e ** is the last token) - we match.         
            if (this._subEvaluator.EvaluatorCount == 0)
            {
                newPosition = allChars.Length;
                return true;
            }

            // Because we know we have more tokens in the pattern (subevaluators) - those will require a minimum amount of characters to match (could be 0 too).
            // We can therefore calculate a "max" character position that we can match to, as if we exceed that position the remaining tokens cant possibly match.
            var maxPos = (allChars.Length - _subEvaluator.ConsumesMinLength);

            // If all of the remaining tokens have a precise length, we can calculate the exact character that we need to macth to in the string.
            // Otherwise we have to test at multiple character positions until we find a match (less efficient)
            if (!_subEvaluator.ConsumesVariableLength)
            {
                // Fixed length.
                // As we can only match full segments, make sure character before chacracter at max pos is a seperator, 
                if(maxPos > 0)
                {
                    var mustMatchUntilChar = allChars[maxPos - 1];
                    if (mustMatchUntilChar != '/' && mustMatchUntilChar != '\\')
                    {
                        // can only match full segments.
                        return false;
                    }
                }
              

                // Advance position to max pos.
                currentPosition = maxPos;
                return _subEvaluator.IsMatch(allChars, currentPosition, out newPosition);
            }
            else
            {
                // Remaining tokens match a variable length of the test string.
                // We iterate each position (within acceptable range) and test at each position.
                bool isMatch;

                currentChar = allChars[currentPosition];

                // If the ** token was parsed with a trailing slash - i.e "**/" then we need to match past it before we test remainijng tokens.
                if (_token.TrailingPathSeperator != null)
                {
                    if (currentChar == '/' || currentChar == '\\')
                    {
                        // match the seperator.
                        currentPosition = currentPosition + 1;
                    }
                }

                // Match until maxpos, is reached.
                while (currentPosition <= maxPos)
                {
                    // Test at current position.
                    isMatch = _subEvaluator.IsMatch(allChars, currentPosition, out newPosition);
                    if (isMatch)
                    {
                        return isMatch;
                    }

                    if(currentPosition == maxPos)
                    {
                        return false;
                    }

                    // Iterate until we hit a seperator or maxPos.
                    while (currentPosition < maxPos)
                    {
                        currentPosition = currentPosition + 1;
                        currentChar = allChars[currentPosition];
                        if (currentChar == '/' || currentChar == '\\')
                        {
                            // advance past the seperator.
                            currentPosition = currentPosition + 1;
                            break;
                        }
                    }
                }
            }

            return false;

        }

        public virtual int ConsumesMinLength
        {
            get { return _subEvaluator.ConsumesMinLength; }
        }

        public bool ConsumesVariableLength
        {
            get { return true; }
        }

        #endregion

    }
}