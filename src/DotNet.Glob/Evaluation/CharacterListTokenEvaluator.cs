using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public class CharacterListTokenEvaluator : IGlobTokenEvaluator
    {
        private readonly CharacterListToken _token;

        public CharacterListTokenEvaluator(CharacterListToken token)
        {
            _token = token;
        }
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            var currentChar = allChars[currentPosition];
            newPosition = currentPosition + 1;

            //var currentChar = (char)read;
            var contains = _token.Characters.Contains(currentChar);
            if (_token.IsNegated)
            {
                if (contains)
                {
                    return false;
                }
            }
            else
            {
                if (!contains)
                {
                    return false;
                }
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