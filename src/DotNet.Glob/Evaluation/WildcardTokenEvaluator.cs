using DotNet.Globbing.Token;
using System;
using System.Text;

namespace DotNet.Globbing.Evaluation
{
    public class WildcardTokenEvaluator : IGlobTokenEvaluator
    {
        private readonly WildcardToken _token;
        //private readonly IGlobToken[] _subTokens;
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
        {

            newPosition = currentPosition;

            // If the glob is last in a pattern, there is no remaining patterns that need matching.
            // In that case we just match every thing until we reach a seperator, at which point we fail.

            if (!_requiresSubEvaluation)
            {
                // if we are at the end of the string, we match!
                if (currentPosition >= allChars.Length)
                {
                    return true;
                }

                for (int i = currentPosition; i <= allChars.Length -1; i++)
                {
                    var currentChar = allChars[i];
                    if (currentChar == '/' || currentChar == '\\')
                    {
                        return false;
                    }                   
                }

                newPosition = currentPosition + allChars.Length;
                return true;

            }

            // We have remaining pattern to evaluate.
            if (!_subEvaluator.ConsumesVariableLength)
            {
                // The remaining tokens match against a fixed length string, so wildcard **must** consume
                // a known amount of characters in order for this to have a chance of successful match.
                // but can't consume past / behind current position!
                //var matchLength = (allChars.Length - _subEvaluator.ConsumesMinLength);
                //if (matchLength < currentPosition)
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
            // us past the min required length required by the sub evaluator tokens, and as we are not a directory wildcard, we
            // can't go past a path seperator.

            var maxPos = (allChars.Length - _subEvaluator.ConsumesMinLength);
            for (int i = currentPosition; i <= maxPos; i++)
            {
                var currentChar = allChars[i];
                               
                  

                //int newSubPosition;
                var isMatch = _subEvaluator.IsMatch(allChars, i, out newPosition);
                if (isMatch)
                {
                    return true;
                }

                if (currentChar == '/' || currentChar == '\\')
                {
                    return false;
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