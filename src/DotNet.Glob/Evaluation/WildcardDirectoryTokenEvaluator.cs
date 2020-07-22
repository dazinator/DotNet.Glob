﻿using DotNet.Globbing.Token;
using System;

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
#if SPAN
        {
            return IsMatch(allChars.AsSpan(), currentPosition, out newPosition);
        }
        public bool IsMatch(ReadOnlySpan<char> allChars, int currentPosition, out int newPosition)
#endif
        {
            // We shortcut to success for a ** in some special cases:-
            //  1. The remaining tokens don't need to consume a minimum number of chracters in order to match.

            // We shortcut to failure for a ** in some special cases:-
            // A) The token was parsed with a leading path separator (i.e '/**' and the current charater we are matching from isn't a path separator.

            newPosition = currentPosition;          

            // A) If leading seperator then current character needs to be that seperator.
            if(allChars.Length <= currentPosition || currentPosition < 0)
            {
                return false;
            }
            char currentChar = allChars[currentPosition];
            if (_token.LeadingPathSeparator != null)
            {              
                if (!GlobStringReader.IsPathSeparator(currentChar))
                {
                    // expected separator.
                    return false;
                }
                //else
                //{
                // advance current position to match the leading separator.
                currentPosition = currentPosition + 1;
                //}
            }
            else
            {
                // no leading seperator, means ** used at start of pattern not /** used within pattern.              
                // If **/ is used for start of pattern then input string doesn't need to start with a / or \ and it will be matched.
                // i.e **/foo/bar will match foo/bar or /foo/bar.
                //     where as /**/foo/bar will not match foo/bar it will only match /foo/bar.
                currentChar = allChars[currentPosition];
                if (GlobStringReader.IsPathSeparator(currentChar))
                {
                    // advance current position to match the leading separator.
                    currentPosition = currentPosition + 1;
                }
            }           

            // 1. if no more tokens require matching we match.         
            if (_subEvaluator.ConsumesMinLength == 0)
            {
                newPosition = allChars.Length;
                return true;
            }

            // Because we know we have more tokens in the pattern (subevaluators) - those will require a minimum amount of characters to match (could be 0 too).
            // We can therefore calculate a "max" character position that we can match to, as if we exceed that position the remaining tokens cant possibly match.
            int maxPos = (allChars.Length - _subEvaluator.ConsumesMinLength);

            // If all of the remaining tokens have a precise length, we can calculate the exact character that we need to macth to in the string.
            // Otherwise we have to test at multiple character positions until we find a match (less efficient)
            if (!_subEvaluator.ConsumesVariableLength)
            {
                // Fixed length.
                // As we can only match full segments, make sure character before chacracter at max pos is a separator, 
                if (maxPos > 0)
                {
                    char mustMatchUntilChar = allChars[maxPos - 1];
                    if (!GlobStringReader.IsPathSeparator(mustMatchUntilChar))
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
                // special exception if **/ is at start of pattern, as then the input string need not have any path separators.
                if (_token.TrailingPathSeparator != null)
                {
                    if (GlobStringReader.IsPathSeparator(currentChar))
                    {
                        // match the separator.
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

                    if (currentPosition == maxPos)
                    {
                        return false;
                    }

                    // Iterate until we hit a separator or maxPos.
                    while (currentPosition < maxPos)
                    {
                        currentPosition = currentPosition + 1;
                        currentChar = allChars[currentPosition];

                        if (GlobStringReader.IsPathSeparator(currentChar))
                        {
                            // match the separator.
                            currentPosition = currentPosition + 1;
                            break;
                        }
                    }
                }
            }

            return false;

        }

        public virtual int ConsumesMinLength => _subEvaluator.ConsumesMinLength;

        public bool ConsumesVariableLength => true;

        #endregion

    }
}