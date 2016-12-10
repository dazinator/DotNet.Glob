using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Generation
{
    public class GlobMatchStringGenerator : IGlobTokenVisitor
    {
        private StringBuilder _stringBuilder;
        private Random _random;
        private MatchGenerationMode _mode;

        private List<IMatchGenerator> _generators;

        private enum MatchGenerationMode
        {
            Match, // generates a string that will match all tokens.
            PartialMatch, // geenrates a string where some tokens will match, but others wont.
            NoMatch // generates a string where no portion of it will match any tokens - except wildcards.
        }

        public GlobMatchStringGenerator(IEnumerable<IGlobToken> tokens)
        {
            _stringBuilder = new StringBuilder();
            _random = new Random();
            _generators = new List<IMatchGenerator>(tokens.Count());
            foreach (var token in tokens)
            {
                token.Accept(this);
            }
        }

        public string GenerateRandomMatch()
        {
            //_mode = MatchGenerationMode.Match;
            _stringBuilder.Clear();
            foreach (var generator in _generators)
            {
                generator.Append(_stringBuilder);
            }
            return _stringBuilder.ToString();
        }

        void IGlobTokenVisitor.Visit(CharacterListToken token)
        {
            if (_mode != MatchGenerationMode.Match)
            {
                throw new NotImplementedException();
            }

            _generators.Add(new CharacterListTokenMatchGenerator(token, _random));
        }

        void IGlobTokenVisitor.Visit(PathSeperatorToken token)
        {
            _generators.Add(new PathSeperatorMatchGenerator(token, _random));
        }

        void IGlobTokenVisitor.Visit(LiteralToken token)
        {
            _generators.Add(new LiteralTokenMatchGenerator(token, _random));
        }

        void IGlobTokenVisitor.Visit(LetterRangeToken token)
        {
            _generators.Add(new LetterRangeTokenMatchGenerator(token, _random));
        }

        void IGlobTokenVisitor.Visit(NumberRangeToken token)
        {
            _generators.Add(new NumberRangeTokenMatchGenerator(token, _random));
        }

        void IGlobTokenVisitor.Visit(AnyCharacterToken token)
        {
            _generators.Add(new AnyCharacterTokenMatchGenerator(token, _random));
        }

        void IGlobTokenVisitor.Visit(WildcardToken token)
        {
            _generators.Add(new WildcardTokenMatchGenerator(token, _random));
        }

        public void Visit(WildcardDirectoryToken token)
        {
            _generators.Add(new WildcardDirectoryTokenMatchGenerator(token, _random));
        }
    }
}