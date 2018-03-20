using DotNet.Globbing.Token;
using System.Collections.Generic;

namespace DotNet.Globbing
{
    public interface IGlobBuilder
    {
        IGlobBuilder PathSeperator(PathSeperatorKind kind = PathSeperatorKind.ForwardSlash);
        IGlobBuilder Literal(string text);
        IGlobBuilder AnyCharacter();
        IGlobBuilder Wildcard();
        IGlobBuilder DirectoryWildcard(PathSeperatorKind? leadingPathSeperatorKind = PathSeperatorKind.ForwardSlash, PathSeperatorKind ? trailingPathSeperatorKind = PathSeperatorKind.ForwardSlash);
        IGlobBuilder OneOf(params char[] characters);
        IGlobBuilder NotOneOf(params char[] characters);
        IGlobBuilder LetterInRange(char start, char end);
        IGlobBuilder LetterNotInRange(char start, char end);
        IGlobBuilder NumberInRange(char start, char end);
        IGlobBuilder NumberNotInRange(char start, char end);
        Glob ToGlob(GlobOptions options = null);
        List<IGlobToken> Tokens { get; }
    }

}
