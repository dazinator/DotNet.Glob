using DotNet.Globbing.Token;
using System;
using System.Runtime.CompilerServices;

namespace DotNet.Globbing.Evaluation
{

    public class CharacterListTokenEvaluator : IGlobTokenEvaluator
    {
        private readonly CharacterListToken _token;


        public CharacterListTokenEvaluator(CharacterListToken token)
        {
            _token = token;
        }

#if SPAN
        public bool IsMatch(ReadOnlySpan<char> allChars, int currentPosition, out int newPosition)
#else
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
#endif
        {
            var currentChar = allChars[currentPosition];
            newPosition = currentPosition + 1;

            bool contains = IsMatch(currentChar);

            if (_token.IsNegated)
            {
                return !contains;
            }
            else
            {
                return contains;
            }
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool IsMatch(char containsChar)
        {
            foreach (var item in _token.Characters)
            {
                if (item.Equals(containsChar))
                {
                    return true;
                }
            }

            return false;
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