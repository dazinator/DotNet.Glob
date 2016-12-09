using System;
using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Generation
{
    internal class AnyCharacterTokenMatchGenerator : IMatchGenerator
    {
        private AnyCharacterToken token;
        private Random _random;

        public AnyCharacterTokenMatchGenerator(AnyCharacterToken token, Random _random)
        {
            this.token = token;
            this._random = _random;
        }

        public void Append(StringBuilder builder)
        {
            // append a random single literal char.
            builder.AppendRandomLiteralCharacter(_random);
        }

    }
}