using System;
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

#if SPAN
        public bool IsMatch(ReadOnlySpan<char> allChars, int currentPosition, out int newPosition)
#else
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
#endif
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