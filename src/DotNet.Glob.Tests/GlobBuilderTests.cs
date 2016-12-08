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
            //       /foo?\\*[abc][!1-3].txt
            var glob = new GlobBuilder()
                .PathSeperator()
                .Literal("foo")
                .AnyCharacter()
                .PathSeperator(PathSeperatorKind.BackwardSlash)
                .Wildcard()
                .OneOf('a', 'b', 'c')
                .NumberNotInRange('1', '3')
                .Literal(".txt")
                .ToGlob();

            
            // now format the glob.
            var sut = new GlobTokenFormatter();
            var globString = sut.Format(glob.Tokens);
            Assert.Equal("/foo?\\*[abc][!1-3].txt", globString);

        }

    }
}