using DotNet.Globbing.Token;
using System;

namespace DotNet.Globbing.Evaluation
{
    public class AnyCharacterTokenEvaluator : IGlobTokenEvaluator
    {
        private readonly AnyCharacterToken _token;

        public AnyCharacterTokenEvaluator(AnyCharacterToken token)
        {
            _token = token;
        }

#if NETCOREAPP2_1
        public bool IsMatch(string allChars, int currentPosition, out int newPosition) => IsMatch(allChars.AsSpan(), currentPosition, out newPosition);

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