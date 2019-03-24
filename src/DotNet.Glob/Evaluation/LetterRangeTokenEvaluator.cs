using DotNet.Globbing.Token;
using System;
using System.Runtime.CompilerServices;

namespace DotNet.Globbing.Evaluation
{
    public class LetterRangeTokenEvaluator : IGlobTokenEvaluator
    {     
        private readonly LetterRangeToken _token;

        public LetterRangeTokenEvaluator(LetterRangeToken token)
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
            char currentChar;          
            currentChar = allChars[currentPosition];
            return IsMatch(currentChar);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool IsMatch(char testChar)
        {
            bool isMatch = testChar >= _token.Start && testChar <= _token.End;

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