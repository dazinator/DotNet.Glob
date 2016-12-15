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
            if (!_subEvaluator.ConsumesVariableLength)
            {
                // The remaining tokens match against a fixed length string, so wildcard **must** consume
                // a known amount of characters in order for this to have a chance of successful match.
                // but can't consume past current position!
                // var matchLength = (allChars.Length - _subEvaluator.ConsumesMinLength);
                //if(matchLength < currentPosition)
                //{
                //    return false;
                //}
                //var isMatch = _subEvaluator.IsMatch(allChars, matchLength, out newPosition);
                //return isMatch;
                var isMatch = _subEvaluator.IsMatch(allChars, (allChars.Length - _subEvaluator.ConsumesMinLength), out newPosition);
                return isMatch;
            }

            // if we are at the end of the string, we match!
            if (currentPosition >= allChars.Length)
            {
                return true;
            }

            // otherwise we can consume a variable amount of characters but we can't match more characters than the amount that will take
            // us past the min required length required by the sub evaluator tokens, and as we are a directory wildcard, we
            // can go past a path seperators so no need to check for those, unlike the wildcard evaulator which must stop at them.

            var maxPos = (allChars.Length - _subEvaluator.ConsumesMinLength);
            for (int i = currentPosition; i <= maxPos; i++)
            {
                //int newSubPosition;
                var isMatch = _subEvaluator.IsMatch(allChars, i, out newPosition);
                if (isMatch)
                {
                    return true;
                }
            }

            return false;
         
        }

        public virtual int ConsumesMinLength
        {
            get { return 0; }
        }

        public bool ConsumesVariableLength
        {
            get { return true; }
        }

        #endregion
        
    }
}