using System;
using System.Runtime.CompilerServices;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public class LiteralTokenEvaluatorCaseInsensitive : IGlobTokenEvaluator
    {

        private readonly LiteralToken _token;
        private readonly string _literalAsUpperInvariant;

        public LiteralTokenEvaluatorCaseInsensitive(LiteralToken token)
        {
            _token = token;
            _literalAsUpperInvariant = token.Value.ToUpperInvariant();
        }

#if SPAN
        public bool IsMatch(ReadOnlySpan<char> allChars, int currentPosition, out int newPosition)
#else
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
#endif
        {
            newPosition = currentPosition;
            int counter = 0;

            while (newPosition < allChars.Length && counter < _token.Value.Length)
            {
                var currentChar = allChars[newPosition];
                if (!IsMatch(currentChar, counter))
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

            return true;
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool IsMatch(char containsChar, int position)
        {
            var upperInvariantChar = Char.ToUpperInvariant(containsChar);
            var comparisonChar = _literalAsUpperInvariant[position];
            return upperInvariantChar == comparisonChar;
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