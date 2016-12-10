using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public class NumberRangeTokenEvaluator : IGlobTokenEvaluator
    {
        private readonly NumberRangeToken _token;

        public NumberRangeTokenEvaluator(NumberRangeToken token)
        {
            _token = token;
        }
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            var currentChar = allChars[currentPosition];
            newPosition = currentPosition + 1;

            //var currentChar = (char)read;
            if (currentChar >= _token.Start && currentChar <= _token.End)
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
    }
}