using DotNet.Globbing.Token;
using System;
using Xunit;

namespace DotNet.Globbing.Tests
{
    public class TokeniserTests
    {
       
        [Theory]
        [InlineData("path/hatstand", typeof(LiteralToken), typeof(PathSeperatorToken), typeof(LiteralToken))]
        [InlineData("p*th/ha?s[stu][s-z]and[1-3]/[!a-z]![1234Z]", 
            typeof(LiteralToken), typeof(WildcardToken), typeof(LiteralToken),typeof(PathSeperatorToken),
            typeof(LiteralToken), typeof(AnyCharacterToken), typeof(LiteralToken), typeof(CharacterListToken), typeof(LetterRangeToken), typeof(LiteralToken), typeof(NumberRangeToken), typeof(PathSeperatorToken),
            typeof(LetterRangeToken), typeof(CharacterListToken))]
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