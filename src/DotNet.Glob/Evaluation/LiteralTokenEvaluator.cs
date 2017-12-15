using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public class LiteralTokenEvaluator : IGlobTokenEvaluator
    {
        private readonly bool _caseInsensitive;
        private readonly LiteralToken _token;

        public LiteralTokenEvaluator(LiteralToken token, bool caseInsensitive)
        {
            _token = token;
            _caseInsensitive = caseInsensitive;
        }
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            newPosition = currentPosition;
            int counter = 0;

            while (newPosition < allChars.Length && counter < _token.Value.Length)
            {
                var compareChar = _token.Value[counter];
                var currentChar = allChars[newPosition];

                if (_caseInsensitive)
                {
                    compareChar = char.ToLowerInvariant(compareChar);
                    currentChar = char.ToLowerInvariant(currentChar);
                }

                if (compareChar != currentChar)
                {
                    return false;
                }

                newPosition = newPosition + 1;
                counter = counter + 1;
            }

            if (counter < _token.Value.Length)
            {
                return false;
            }

            //foreach (var literalChar in _token.Value)
            //{
            //    var read = allChars[newPosition];
            //    //if (read == Char.MinValue)
            //    //{
            //    //    return false;
            //    //}
            //    if (read != literalChar)
            //    {
            //        return false;
            //    }
            //    if(currentPosition == )
            //    newPosition = newPosition + 1;

            //}
            return true;
        }

        public virtual int ConsumesMinLength
        {
            get { return _token.Value.Length; }
        }

        public bool ConsumesVariableLength
        {
            get { return false; }
        }
    }
}