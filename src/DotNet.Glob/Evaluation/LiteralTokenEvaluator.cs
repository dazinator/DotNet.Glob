using System;
using System.Runtime.CompilerServices;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public class LiteralTokenEvaluator : IGlobTokenEvaluator
    {

        private readonly LiteralToken _token;

        public LiteralTokenEvaluator(LiteralToken token)
        {
            _token = token;
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
        private bool IsMatch(char containsChar, int position)
        {
            return containsChar == _token.Value[position];
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