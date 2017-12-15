using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public class LetterRangeTokenEvaluator : IGlobTokenEvaluator
    {
        private readonly bool _caseInsensitive;
        private readonly LetterRangeToken _token;

        public LetterRangeTokenEvaluator(LetterRangeToken token, bool caseInsensitive)
        {
            _caseInsensitive = caseInsensitive;
            _token = token;
        }

        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            newPosition = currentPosition + 1;

            //var currentChar = (char)read;
            char start, end, currentChar;
            start = _token.Start;
            end = _token.End;
            currentChar = allChars[currentPosition];

            if (_caseInsensitive)
            {
                start = char.ToLowerInvariant(start);
                end = char.ToLowerInvariant(end);
                currentChar = char.ToLowerInvariant(currentChar);
            }

            if (currentChar >= start && currentChar <= end)
            {
                if (_token.IsNegated)
                {
                    return false; // failed to match
                }
            }
            else
            {
                if (!_token.IsNegated)
                {
                    return false; // failed to match
                }
            }

            return true;
            // this.Success = true;
        }

        public virtual int ConsumesMinLength
        {
            get { return 1; }
        }

        public bool ConsumesVariableLength
        {
            get { return false; }
        }
    }


}