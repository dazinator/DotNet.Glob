using System;
using DotNet.Globbing;
using DotNet.Globbing.Token;
using Xunit;

namespace DotNet.Glob.Tests
{
    public class TokeniserTests
    {
       
        [Theory]
        [InlineData("path/hatstand", typeof(LiteralToken), typeof(PathSeperatorToken), typeof(LiteralToken))]
        [InlineData("p*th/ha?s[stu][s-z]and[1-3]/[!a-z]![1234Z]", 
            typeof(LiteralToken), typeof(WildcardToken), typeof(LiteralToken),typeof(PathSeperatorToken),
            typeof(LiteralToken), typeof(AnyCharacterToken), typeof(LiteralToken), typeof(CharacterListToken), typeof(LetterRangeToken), typeof(LiteralToken), typeof(NumberRangeToken), typeof(PathSeperatorToken),
            typeof(LetterRangeToken), typeof(LiteralToken), typeof(CharacterListToken))]
        [InlineData("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*",
            typeof(LiteralToken), typeof(AnyCharacterToken), typeof(LiteralToken), typeof(PathSeperatorToken),
            typeof(WildcardToken), typeof(LiteralToken), typeof(CharacterListToken), 
            typeof(LiteralToken), typeof(LetterRangeToken), typeof(LiteralToken),
            typeof(NumberRangeToken), typeof(CharacterListToken), typeof(LetterRangeToken),
            typeof(NumberRangeToken), typeof(LiteralToken),
            typeof(WildcardToken))]
        [InlineData("path/**/*.*", typeof(LiteralToken), typeof(WildcardDirectoryToken), typeof(WildcardToken), typeof(LiteralToken), typeof(WildcardToken))]
        public void Can_Tokenise_Glob_Pattern(string testString, params Type[] expectedTokens)
        {
            // Arrange

            var sut = new GlobTokeniser();
            var tokens = sut.Tokenise(testString);

            Assert.True(tokens.Count == expectedTokens.Length);
            for (int i = 0; i < tokens.Count; i++)
            {
                Assert.True(tokens[i].GetType() == expectedTokens[i]);

            }

        }

    }
}