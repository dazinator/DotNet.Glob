using DotNet.Globbing.Token;
using System;
using System.Text;

namespace DotNet.Globbing.Evaluation
{
    public class WildcardTokenEvaluator : IGlobTokenEvaluator
    {
        private readonly WildcardToken _token;
        private readonly CompositeTokenEvaluator _subEvaluator;
        private readonly bool _requiresSubEvaluation;
      

        public WildcardTokenEvaluator(WildcardToken token, CompositeTokenEvaluator subEvaluator)
        {
            _token = token;
            _subEvaluator = subEvaluator;
            _requiresSubEvaluation = _subEvaluator.EvaluatorCount > 0;          
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

            newPosition = currentPosition;


            if (!_requiresSubEvaluation) // We are the last token in the pattern
            {
                // If we have reached the end of the string, then we match.
                if (currentPosition >= allChars.Length)
                {
                    return true;
                }

                // We dont match if the remaining string has separators.
                for (int i = currentPosition; i <= allChars.Length - 1; i++)
                {
                    var currentChar = allChars[i];
                    if (currentChar == '/' || currentChar == '\\')
                    {
                        return false;
                    }
                }

                // we have matched upto the new position.
                newPosition = currentPosition + allChars.Length;
                return true;

            }

            // We are not the last token in the pattern, and so the _subEvaluator representing the remaining pattern tokens must also match.
            // Does the sub pattern match a fixed length string, or variable length string?
            if (!_subEvaluator.ConsumesVariableLength)
            {
                // The remaining tokens match against a fixed length string, so we can infer that this wildcard **must** match
                // a fixed amount of characters in order for the subevaluator to match its fixed amount of characters from the remaining portion
                // of the string. 
                // So we must match upto that position. We can't match separators. 
                var requiredMatchPosition = allChars.Length - _subEvaluator.ConsumesMinLength;
                //if (requiredMatchPosition < currentPosition)
                //{
                //    return false;
                //}
                for (int i = currentPosition; i < requiredMatchPosition; i++)
                {
                    var currentChar = allChars[i];
                    if (currentChar == '/' || currentChar == '\\')
                    {
                        return false;
                    }
                }
                var isMatch = _subEvaluator.IsMatch(allChars, requiredMatchPosition, out newPosition);
                return isMatch;
            }

            // We can match a variable amount of characters but,
            // We can't match more characters than the amount that will take us past the min required length required by the sub evaluator tokens,
            // and as we are not a directory wildcard, we can't match past a path separator.
            var maxPos = allChars.Length - 1;
            if (_subEvaluator.ConsumesMinLength > 0)
            {
                maxPos = maxPos - _subEvaluator.ConsumesMinLength + 1;
            }
           // var maxPos = (allChars.Length - _subEvaluator.ConsumesMinLength);
            for (int i = currentPosition; i <= maxPos; i++)
            {               

                var isMatch = _subEvaluator.IsMatch(allChars, i, out newPosition);
                if (isMatch)
                {
                    return true;
                }

                var currentChar = allChars[i];
                if (currentChar == '/' || currentChar == '\\')
                {
                    return false;
                }
            }

            // If subevakuators are optional match then match
            if (_subEvaluator.ConsumesMinLength == 0)
            {
                return true;
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