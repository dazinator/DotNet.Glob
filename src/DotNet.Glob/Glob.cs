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
      
        public Glob(params IGlobToken[] tokens)
        {
            Tokens = tokens;
            _Formatter = new GlobTokenFormatter();
            _isMatchEvaluator = new GlobTokenEvaluator(Tokens);        
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
            return new Glob(tokens.ToArray());
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
