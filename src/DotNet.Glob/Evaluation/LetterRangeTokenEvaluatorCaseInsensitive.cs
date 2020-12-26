using System;
using System.Runtime.CompilerServices;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public class LetterRangeTokenEvaluatorCaseInsensitive : IGlobTokenEvaluator
    {
        private readonly LetterRangeToken _token;
        private readonly char _startUpperInvariant;
        private readonly char _endUpperInvariant;
        private readonly char _startLowerInvariant;
        private readonly char _endLowerInvariant;

        public LetterRangeTokenEvaluatorCaseInsensitive(LetterRangeToken token)
        {
            _token = token;
            _startUpperInvariant = char.ToUpperInvariant(token.Start);
            _endUpperInvariant = char.ToUpperInvariant(token.End);
            _startLowerInvariant = char.ToLowerInvariant(token.Start);
            _endLowerInvariant = char.ToLowerInvariant(token.End);
        }

#if SPAN
        public bool IsMatch(ReadOnlySpan<char> allChars, int currentPosition, out int newPosition)
#else
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
#endif
        {
            newPosition = currentPosition + 1;
            char currentChar;
            currentChar = allChars[currentPosition];
            return IsMatch(currentChar);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool IsMatch(char containsChar)
        {
            bool isMatch = (containsChar >= _startUpperInvariant && containsChar <= _endUpperInvariant)
                || (containsChar >= _startLowerInvariant && containsChar <= _endLowerInvariant);

            if (_token.IsNegated)
            {
                return !isMatch;
            }
            else
            {
                return isMatch;
            }
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