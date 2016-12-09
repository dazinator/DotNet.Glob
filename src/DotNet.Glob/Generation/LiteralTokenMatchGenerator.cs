using System.Text;
using DotNet.Globbing.Token;
using System;

namespace DotNet.Globbing.Generation
{
    internal class LiteralTokenMatchGenerator : IMatchGenerator
    {
        private LiteralToken token;
        private Random _random;

        public LiteralTokenMatchGenerator(LiteralToken token, Random _random)
        {
            this.token = token;
            this._random = _random;
        }

        public void Append(StringBuilder builder)
        {
            builder.Append(token.Value);
        }
    }
}