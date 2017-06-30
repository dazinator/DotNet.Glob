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

            newPosition = currentPosition;
            //if (!_subEvaluator.ConsumesVariableLength)


            // if we are at the end of the string, we match!
            if (currentPosition >= allChars.Length)
            {
                return true;
            }


            // The max pos we can match upto in the string, because of subtoken match requirements.
            var maxPos = (allChars.Length - _subEvaluator.ConsumesMinLength);
            // we can only match full segments at a time, where as match pos could point to a char in the middle of a segment.

            if (!_subEvaluator.ConsumesVariableLength)
            {
                // must match a fixed length of the string (must match all to max pos), but we can only match entire segments, 
                // so for a successful match, the char before maxpos must either be current pos (we match nothing), or a seperator.
                if (maxPos -1 != currentPosition)
                {
                    var precedingchar = allChars[maxPos - 1];
                    if (precedingchar != '/' && precedingchar != '\\')
                    {
                        return false;                      
                    }
                }

                return _subEvaluator.IsMatch(allChars, maxPos, out newPosition);
            }
            else
            {
                // can match a variable length, but in compelte segments at a time, not past max pos.

                int pos = currentPosition;

                bool isMatch;

                var currentChar = allChars[pos];

                var isSeprator = currentChar == '/' || currentChar == '\\';

                if (this._token.LeadingPathSeperator != null && !isSeprator)
                {
                    // must begin with seperator.
                    return false;
                }

                if (currentChar == '/' || currentChar == '\\')
                {
                    if (pos == maxPos)
                    {
                        return false;
                    }
                    pos = pos + 1;
                }


                while (pos <= maxPos)
                {


                    isMatch = _subEvaluator.IsMatch(allChars, pos, out newPosition);
                    if (isMatch)
                    {
                        return isMatch;
                    }

                    currentChar = allChars[pos];
                    while (currentChar != '/' && currentChar != '\\' && pos + 1 < maxPos)
                    {
                        pos = pos + 1;
                        currentChar = allChars[pos];
                    }

                    pos = pos + 1;


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