using System;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{

    public class PathSeparatorTokenEvaluator : IGlobTokenEvaluator
    {
        private readonly PathSeparatorToken _token;

        public PathSeparatorTokenEvaluator(PathSeparatorToken token)
        {
            _token = token;
        }
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
#if SPAN
        {
            return IsMatch(allChars.AsSpan(), currentPosition, out newPosition);
        }
        public bool IsMatch(ReadOnlySpan<char> allChars, int currentPosition, out int newPosition)
#endif
        {
            var currentChar = allChars[currentPosition];
            newPosition = currentPosition + 1;
            return GlobStringReader.IsPathSeparator(currentChar);
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