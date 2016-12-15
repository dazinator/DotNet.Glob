using DotNet.Globbing.Token;
using System;
using System.Collections.Generic;
using Microsoft.DotNet.ProjectModel.FileSystemGlobbing.Internal.PathSegments;
using Xunit;

namespace DotNet.Globbing.Tests
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
                .PathSeperator()
                .Literal("foo")
                .AnyCharacter()
                .PathSeperator(PathSeperatorKind.BackwardSlash)
                .Wildcard()
                .OneOf('a', 'b', 'c')
                .NumberNotInRange('1', '3')
                .PathSeperator()
                .DirectoryWildcard(PathSeperatorKind.ForwardSlash)              
                .Wildcard()
                .Literal(".txt")
                .Tokens;

            Assert.Equal(11, tokens.Count);
            Assert.True(tokens[0] is PathSeperatorToken);
            Assert.True(tokens[1] is LiteralToken);
            Assert.True(tokens[2] is AnyCharacterToken);
            Assert.True(tokens[3] is PathSeperatorToken);
            Assert.True(tokens[4] is WildcardToken);
            Assert.True(tokens[5] is CharacterListToken);
            Assert.True(tokens[6] is NumberRangeToken);
            Assert.True(tokens[7] is PathSeperatorToken);
            Assert.True(tokens[8] is WildcardDirectoryToken);           
            Assert.True(tokens[9] is WildcardToken);
            Assert.True(tokens[10] is LiteralToken);
        }

    }
}