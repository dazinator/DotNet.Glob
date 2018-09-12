using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public class AnyCharacterTokenEvaluator : IGlobTokenEvaluator
    {
        private readonly AnyCharacterToken _token;

        public AnyCharacterTokenEvaluator(AnyCharacterToken token)
        {
            _token = token;
        }

        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            newPosition = currentPosition + 1;
            var currentChar = allChars[currentPosition];
            if (GlobStringReader.IsPathSeparator(currentChar))
            {
                return false;
            }

            return true;
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