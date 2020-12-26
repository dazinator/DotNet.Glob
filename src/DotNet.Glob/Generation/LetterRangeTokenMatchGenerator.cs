using System;
using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Generation
{
    internal class LetterRangeTokenMatchGenerator : IMatchGenerator
    {
        private LetterRangeToken token;
        private Random _random;

        public LetterRangeTokenMatchGenerator(LetterRangeToken token, Random _random)
        {
            this.token = token;
            this._random = _random;
        }

        public void AppendMatch(StringBuilder builder)
        {
            if (token.IsNegated)
            {
                builder.Append(_random.GetRandomLetterCharacterNotBetween(token.Start, token.End));
            }
            else
            {
                builder.Append(_random.GetRandomCharacterBetween(token.Start, token.End));
            }
        }

        public void AppendNonMatch(StringBuilder builder)
        {
            if (token.IsNegated)
            {
                builder.Append(_random.GetRandomCharacterBetween(token.Start, token.End));
            }
            else
            {
                builder.Append(_random.GetRandomLetterCharacterNotBetween(token.Start, token.End));
            }
        }

    }
}