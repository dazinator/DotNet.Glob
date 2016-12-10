using DotNet.Globbing.Token;
using System;
using System.Text;

namespace DotNet.Globbing.Generation
{
    internal class WildcardDirectoryTokenMatchGenerator : IMatchGenerator
    {
        private WildcardDirectoryToken token;
        private Random _random;
        private PathSeperatorKind _PathSeperatorKind = PathSeperatorKind.ForwardSlash;

        private int _maxLiteralLength = 10;
        private int _maxSegments = 5;
        private int _minSegments = 0;

        public WildcardDirectoryTokenMatchGenerator(WildcardDirectoryToken token, Random _random)
        {
            this.token = token;
            this._random = _random;
        }

        public void Append(StringBuilder builder)
        {
            // append a random number of random literals, between 0 characters and 10 in length,
            // seperated by path seperators.
            var numberOfSegments = _random.Next(_minSegments, _maxSegments);
            if (numberOfSegments == 0)
            {
                return;
            }
            if (numberOfSegments > 1)
            {
                for (int i = 1; i <= (numberOfSegments - 1); i++)
                {
                    builder.AppendRandomLiteralString(_random, _maxLiteralLength);
                    if (_PathSeperatorKind == PathSeperatorKind.ForwardSlash)
                    {
                        builder.Append('/');
                    }
                    else
                    {
                        builder.Append('\\');
                    }
                }
            }

            builder.AppendRandomLiteralString(_random, _maxLiteralLength);

        }

    }
}