using System;
using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Generation
{
    internal class NumberRangeTokenMatchGenerator : IMatchGenerator
    {
        private NumberRangeToken token;
        private Random _random;

        public NumberRangeTokenMatchGenerator(NumberRangeToken token, Random _random)
        {
            this.token = token;
            this._random = _random;
        }

        public void Append(StringBuilder builder)
        {
            if (token.IsNegated)
            {
                builder.Append(GetRandomLiteralCharacterNotBetween(token.Start, token.End));
            }
            else
            {
                builder.Append(GetRandomCharacterBetween(token.Start, token.End));
            }
        }

        public char GetRandomCharacterBetween(char start, char end)
        {
            return (char)_random.Next((int)start, (int)end);
        }

        public char GetRandomLiteralCharacterNotBetween(char start, char end)
        {

            var generateHigher = _random.NextDouble() > 0.5;
            if ((int)end >= 'z')
            {
                generateHigher = false; // end char is already z or greater so attempt to generate lower.
            }
            if (generateHigher)
            {
                // generating a random letter thats higher than the end char.
                return (char)_random.Next(end >= 'a' ? end : 'a', 'z');
            }
            if ((int)start <= '0')
            {
                throw new NotImplementedException("Could not generate a random literal character that is outside the specified range: (start - end) = " + start + " - " + end);
            }
            // generating a random digit thats lower than the start char.
            return (char)_random.Next('0', start <= '9' ? start : '9');

        }
    }
}