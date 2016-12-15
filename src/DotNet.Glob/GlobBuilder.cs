using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.Globbing.Token;

namespace DotNet.Globbing
{
    public class GlobBuilder : IGlobBuilder
    {
        private readonly List<IGlobToken> _tokens;

        public GlobBuilder()
        {
            _tokens = new List<IGlobToken>();
        }

        public IGlobBuilder AnyCharacter()
        {
            _tokens.Add(new AnyCharacterToken());
            return this;
        }

        public IGlobBuilder OneOf(params char[] characters)
        {
            _tokens.Add(new CharacterListToken(characters, false));
            return this;
        }

        public IGlobBuilder Literal(string text)
        {
            _tokens.Add(new LiteralToken(text));
            return this;
        }

        public IGlobBuilder NotOneOf(params char[] characters)
        {
            _tokens.Add(new CharacterListToken(characters, true));
            return this;
        }

        public IGlobBuilder PathSeperator(PathSeperatorKind kind = PathSeperatorKind.ForwardSlash)
        {
            char seperatorChar;
            switch (kind)
            {
                case PathSeperatorKind.ForwardSlash:
                    seperatorChar = '/';
                    break;
                case PathSeperatorKind.BackwardSlash:
                    seperatorChar = '\\';
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind));
            }
            _tokens.Add(new PathSeperatorToken(seperatorChar));
            return this;
        }

        public IGlobBuilder Wildcard()
        {
            _tokens.Add(new WildcardToken());
            return this;
        }

        public IGlobBuilder DirectoryWildcard(PathSeperatorKind? trailingSeperatorKind = PathSeperatorKind.ForwardSlash)
        {
            if (trailingSeperatorKind == null)
            {
                _tokens.Add(new WildcardDirectoryToken(null));
            }
            else
            {
                switch (trailingSeperatorKind)
                {
                    case PathSeperatorKind.BackwardSlash:
                        _tokens.Add(new WildcardDirectoryToken('\\'));
                        break;
                    case PathSeperatorKind.ForwardSlash:
                        _tokens.Add(new WildcardDirectoryToken('/'));
                        break;
                    default:
                        break;
                }
            }


            return this;
        }

        public IGlobBuilder LetterInRange(char start, char end)
        {
            _tokens.Add(new LetterRangeToken(start, end, false));
            return this;
        }

        public IGlobBuilder LetterNotInRange(char start, char end)
        {
            _tokens.Add(new LetterRangeToken(start, end, true));
            return this;
        }

        public IGlobBuilder NumberInRange(char start, char end)
        {
            _tokens.Add(new NumberRangeToken(start, end, false));
            return this;
        }

        public IGlobBuilder NumberNotInRange(char start, char end)
        {
            _tokens.Add(new NumberRangeToken(start, end, true));
            return this;
        }

        public Glob ToGlob()
        {
            return new Glob(this._tokens.ToArray());
        }

        public List<IGlobToken> Tokens
        {
            get { return _tokens; }
        }


    }
}
