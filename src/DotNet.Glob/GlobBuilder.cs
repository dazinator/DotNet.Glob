using System;
using System.Collections.Generic;
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

        public IGlobBuilder PathSeparator(PathSeparatorKind kind = PathSeparatorKind.ForwardSlash)
        {
            char separatorChar;
            switch (kind)
            {
                case PathSeparatorKind.ForwardSlash:
                    separatorChar = '/';
                    break;
                case PathSeparatorKind.BackwardSlash:
                    separatorChar = '\\';
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind));
            }
            _tokens.Add(new PathSeparatorToken(separatorChar));
            return this;
        }

        public IGlobBuilder Wildcard()
        {
            _tokens.Add(new WildcardToken());
            return this;
        }

        public IGlobBuilder DirectoryWildcard(PathSeparatorKind? leadingSeparatorKind = PathSeparatorKind.ForwardSlash, PathSeparatorKind? trailingSeparatorKind = PathSeparatorKind.ForwardSlash)
        {

            PathSeparatorToken trailingSep = null;
            PathSeparatorToken leadingSep = null;

            if (trailingSeparatorKind == null)
            {
                trailingSep = null;
            }
            else
            {
                switch (trailingSeparatorKind)
                {
                    case PathSeparatorKind.BackwardSlash:
                        trailingSep = new PathSeparatorToken('\\');
                        break;
                    case PathSeparatorKind.ForwardSlash:
                        trailingSep = new PathSeparatorToken('/');
                        break;
                    default:
                        break;
                }
            }

            if (leadingSeparatorKind == null)
            {
                leadingSep = null;
            }
            else
            {
                switch (leadingSeparatorKind)
                {
                    case PathSeparatorKind.BackwardSlash:
                        leadingSep = new PathSeparatorToken('\\');
                        break;
                    case PathSeparatorKind.ForwardSlash:
                        leadingSep = new PathSeparatorToken('/');
                        break;
                    default:
                        break;
                }
            }

            _tokens.Add(new WildcardDirectoryToken(leadingSep, trailingSep));


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

        public Glob ToGlob(GlobOptions options = null)
        {
            return new Glob(options ?? GlobOptions.Default, _tokens.ToArray());
        }

        public List<IGlobToken> Tokens
        {
            get { return _tokens; }
        }


    }
}
