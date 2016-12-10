using System.Collections.Generic;
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
            //if (read == Char.MinValue)
            //{
            //    return;
            //}
            if (GlobStringReader.IsPathSeperator(currentChar))
            {
                return false;
            }

            return true;


        }
    }
}