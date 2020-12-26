using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DotNet.Globbing;
using DotNet.Globbing.Token;

namespace DotNet.Glob.Benchmarks.Utils
{
    /// <summary>
    /// Formats a glob as a Regular expression string.
    /// </summary>
    public class GlobToRegexFormatter : IGlobTokenVisitor
    {
        private StringBuilder _stringBuilder;

        public GlobToRegexFormatter()
        {
            _stringBuilder = new StringBuilder();
        }

        public string Format(IEnumerable<IGlobToken> tokens)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append('^');
            foreach (var token in tokens)
            {
                token.Accept(this);
            }
            _stringBuilder.Append("$");
            return _stringBuilder.ToString();
        }

        public void Visit(WildcardDirectoryToken wildcardDirectoryToken)
        {
            _stringBuilder.Append(".*");
            if (wildcardDirectoryToken.TrailingPathSeparator != null)
            {
                _stringBuilder.Append(@"[/\\]");
            }
        }

        void IGlobTokenVisitor.Visit(CharacterListToken token)
        {
            _stringBuilder.Append('[');
            if (token.IsNegated)
            {
                _stringBuilder.Append('^');
            }
            _stringBuilder.Append(Regex.Escape(new string(token.Characters)));
            _stringBuilder.Append(']');

        }

        void IGlobTokenVisitor.Visit(PathSeparatorToken token)
        {
            _stringBuilder.Append(@"[/\\]");
        }

        void IGlobTokenVisitor.Visit(LiteralToken token)
        {
            _stringBuilder.Append("(");
            _stringBuilder.Append(Regex.Escape(token.Value));
            _stringBuilder.Append(")");
        }

        void IGlobTokenVisitor.Visit(LetterRangeToken token)
        {

            _stringBuilder.Append('[');
            if (token.IsNegated)
            {
                _stringBuilder.Append('^');
            }
            _stringBuilder.Append(Regex.Escape(token.Start.ToString()));
            _stringBuilder.Append('-');
            _stringBuilder.Append(Regex.Escape(token.End.ToString()));
            _stringBuilder.Append(']');
        }

        void IGlobTokenVisitor.Visit(NumberRangeToken token)
        {
            _stringBuilder.Append('[');
            if (token.IsNegated)
            {
                _stringBuilder.Append('^');
            }
            _stringBuilder.Append(Regex.Escape(token.Start.ToString()));
            _stringBuilder.Append('-');
            _stringBuilder.Append(Regex.Escape(token.End.ToString()));
            _stringBuilder.Append(']');
        }

        void IGlobTokenVisitor.Visit(AnyCharacterToken token)
        {
            _stringBuilder.Append(@"[^/\\]{1}");
        }

        void IGlobTokenVisitor.Visit(WildcardToken token)
        {
            _stringBuilder.Append(@"[^/\\]*");
        }
    }
}
