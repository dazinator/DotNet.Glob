using System;
using System.Collections.Generic;
using System.Linq;
using DotNet.Globbing.Evaluation;
using DotNet.Globbing.Token;

namespace DotNet.Globbing
{
    public class Glob
    {
        public IGlobToken[] Tokens { get; }
        private GlobTokenFormatter _Formatter;
        private string _pattern;
        private readonly CompositeEvaluator _isMatchEvaluator;
        private readonly GlobTokenMatchAnalysisEvaluator _matchEvaluator; // provides more in depth analyis than IsMatch.

        public Glob(params IGlobToken[] tokens)
        {
            Tokens = tokens;
            _Formatter = new GlobTokenFormatter();
            _isMatchEvaluator = new CompositeEvaluator(Tokens);
            _matchEvaluator = new GlobTokenMatchAnalysisEvaluator(Tokens);
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
            return _isMatchEvaluator.IsMatch(subject);          
        }

        public MatchInfo Match(string subject)
        {
            // var segments = reader.ReadPathSegment();          
            return _matchEvaluator.Evaluate(subject);           
        }

        public override string ToString()
        {
            if (_pattern == null)
            {
                _pattern = _Formatter.Format(Tokens);
            }
            return _pattern;
        }
    }
}
