using System.Text;
using DotNet.Globbing.Token;
using System;

namespace DotNet.Globbing.Generation
{
    internal class WildcardTokenMatchGenerator : IMatchGenerator
    {
        private WildcardToken token;
        private Random _random;

        public WildcardTokenMatchGenerator(WildcardToken token, Random _random)
        {
            this.token = token;
            this._random = _random;
        }

        public void Append(StringBuilder builder)
        {
            // append a random literal, between 0 characters and 10 in length
            builder.AppendRandomLiteralString(_random, 10);
        }
    }
}