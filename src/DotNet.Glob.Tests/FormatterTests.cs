using DotNet.Globbing.Token;
using System;
using System.Collections.Generic;
using Microsoft.DotNet.ProjectModel.FileSystemGlobbing.Internal.PathSegments;
using Xunit;

namespace DotNet.Globbing.Tests
{
    public class FormatterTests
    {

        [Fact]
        public void Can_Format_Glob_Pattern()
        {
            // we could use the tokeniser here, but deciding to directly
            // build the following glob pattern using tokens:
            //       /foo?\\*[abc][!1-3].txt

            var glob = new Glob(new PathSeperatorToken('/'),
                                new LiteralToken("foo"),
                                new AnyCharacterToken(),
                                new PathSeperatorToken('\\'),
                                new WildcardToken(),
                                new CharacterListToken(new char[] { 'a', 'b', 'c' }, false),
                                new PathSeperatorToken('/'),
                                new WildcardDirectoryToken('/'),                              
                                new NumberRangeToken('1', '3', true),
                                new LiteralToken(".txt"));

            // now format the glob.
            var sut = new GlobTokenFormatter();
            var globString = sut.Format(glob.Tokens);
            Assert.Equal("/foo?\\*[abc]/**/[!1-3].txt", globString);

        }

    }
}