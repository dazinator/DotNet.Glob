using System;
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
        private readonly GlobTokenEvaluator _isMatchEvaluator;
        private readonly bool _caseInsensitive;

        public Glob(bool caseInsensitive, params IGlobToken[] tokens)
        {
            Tokens = tokens;
            _caseInsensitive = caseInsensitive;
            _Formatter = new GlobTokenFormatter();
            _isMatchEvaluator = new GlobTokenEvaluator(_caseInsensitive, Tokens);
        }

        public static Glob Parse(string pattern)
        {
            var options = GlobParseOptions.Default;
            return Parse(pattern, options);
        }

        public static Glob Parse(string pattern, GlobParseOptions options)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException(pattern);
            }
            var tokeniser = new GlobTokeniser();
            var tokens = tokeniser.Tokenise(pattern, options.AllowInvalidPathCharacters);
            return new Glob(options.CaseInsensitive, tokens.ToArray());
        }

        public bool IsMatch(string subject)
        {
            return _isMatchEvaluator.IsMatch(subject);
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
