using DotNet.Globbing.Token;
using System.Collections.Generic;

namespace DotNet.Globbing
{
    public interface IGlobBuilder
    {
        IGlobBuilder PathSeparator(PathSeparatorKind kind = PathSeparatorKind.ForwardSlash);
        IGlobBuilder Literal(string text);
        IGlobBuilder AnyCharacter();
        IGlobBuilder Wildcard();
        IGlobBuilder DirectoryWildcard(PathSeparatorKind? leadingPathSeparatorKind = PathSeparatorKind.ForwardSlash, PathSeparatorKind ? trailingPathSeparatorKind = PathSeparatorKind.ForwardSlash);
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
