using DotNet.Globbing.Token;
using System;
using System.Runtime.CompilerServices;

namespace DotNet.Globbing.Evaluation
{

    public class CharacterListTokenEvaluatorCaseInsensitive : IGlobTokenEvaluator
    {
        private readonly CharacterListToken _token;
        private readonly char[] _charactersAsUpperInvariant;

        public CharacterListTokenEvaluatorCaseInsensitive(CharacterListToken token)
        {           
            _token = token;

            _charactersAsUpperInvariant = new char[token.Characters.Length];
            for (int i = 0; i < token.Characters.Length; i++)
            {
                var tokenChar = token.Characters[i];
                _charactersAsUpperInvariant[i] = Char.ToUpperInvariant(tokenChar);
            }
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
            var upperInvariantChar = Char.ToUpperInvariant(containsChar);

            foreach (var item in _charactersAsUpperInvariant)
            {
                if (item.Equals(upperInvariantChar))
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