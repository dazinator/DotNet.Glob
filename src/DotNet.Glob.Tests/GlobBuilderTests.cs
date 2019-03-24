using DotNet.Globbing;
using DotNet.Globbing.Token;
using Xunit;

namespace DotNet.Glob.Tests
{
    public class GlobBuilderTests
    {

        [Fact]
        public void Can_Build_Glob_Pattern()
        {
            // 
            // build the following glob pattern using glob builder:
            //       /foo?\\*[abc][!1-3]/**/*.txt
            var tokens = new GlobBuilder()
                .PathSeparator()
                .Literal("foo")
                .AnyCharacter()
                .PathSeparator(PathSeparatorKind.BackwardSlash)
                .Wildcard()
                .OneOf('a', 'b', 'c')
                .NumberNotInRange('1', '3')              
                .DirectoryWildcard(PathSeparatorKind.ForwardSlash, PathSeparatorKind.ForwardSlash)              
                .Wildcard()
                .Literal(".txt")
                .Tokens;

            Assert.Equal(10, tokens.Count);
            Assert.True(tokens[0] is PathSeparatorToken);
            Assert.True(tokens[1] is LiteralToken);
            Assert.True(tokens[2] is AnyCharacterToken);
            Assert.True(tokens[3] is PathSeparatorToken);
            Assert.True(tokens[4] is WildcardToken);
            Assert.True(tokens[5] is CharacterListToken);
            Assert.True(tokens[6] is NumberRangeToken);
            Assert.True(tokens[7] is WildcardDirectoryToken);           
            Assert.True(tokens[8] is WildcardToken);
            Assert.True(tokens[9] is LiteralToken);
        }

    }
}