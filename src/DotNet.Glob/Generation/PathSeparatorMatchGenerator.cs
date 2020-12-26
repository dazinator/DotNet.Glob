using System;
using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Generation
{
    internal class PathSeparatorMatchGenerator : IMatchGenerator
    {
        private PathSeparatorToken token;
        private Random _random;

        public PathSeparatorMatchGenerator(PathSeparatorToken token, Random _random)
        {
            this.token = token;
            this._random = _random;
        }

        public void AppendMatch(StringBuilder builder)
        {
            builder.Append(token.Value);
        }

        public void AppendNonMatch(StringBuilder builder)
        {
            builder.AppendRandomLiteralCharacter(_random);
        }
    }
}