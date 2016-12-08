using System;
using System.Collections.Generic;
using System.Linq;
using DotNet.Globbing.Token;

namespace DotNet.Globbing
{
    public class Glob
    {
        public IGlobToken[] Tokens { get; set; }

        public Glob(IGlobToken[] tokens)
        {
            Tokens = tokens;
        }

        public Glob()
        {
        }

        public static Glob Parse(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException(pattern);
            }

            var tokeniser = new GlobTokeniser();
            var tokens = tokeniser.Tokenise(pattern);
            return new Glob(tokens.ToArray());
        }

        public bool IsMatch(string subject)
        {
            // var segments = reader.ReadPathSegment();
            var evaluator = new GlobTokenEvaluator(Tokens);
            var matchInfo = evaluator.Evaluate(subject);
            var success = matchInfo.Success;
            return success;
        }

        public MatchInfo Match(string subject)
        {
            // var segments = reader.ReadPathSegment();
            var evaluator = new GlobTokenEvaluator(Tokens);
            var matchInfo = evaluator.Evaluate(subject);
            return matchInfo;
        }

    }
}
