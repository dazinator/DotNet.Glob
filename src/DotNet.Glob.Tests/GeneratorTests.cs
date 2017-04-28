using DotNet.Globbing.Generation;
using Xunit;

namespace DotNet.Glob.Tests
{
    public class GeneratorTests
    {
        [Theory]
        [InlineData("literal", 1000)]
        [InlineData("a/literal", 1000)]
        [InlineData("path/*atstand", 1000)]
        [InlineData("path/hats*nd", 1000)]
        [InlineData("path/?atstand", 1000)]
        [InlineData("path/?atstand?", 1000)]
        [InlineData("p?th/*a[bcd]", 1000)]
        [InlineData("p?th/*a[bcd]b[e-g]a[1-4]", 1000)]
        [InlineData("p?th/*a[bcd]b[e-g]a[1-4][!wxyz]", 1000)]
        [InlineData("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*", 1000)]
        [InlineData("path/**/somefile.txt", 1000)]
        public void Can_Generate_Non_Matches(string pattern, int volume)
        {
            var glob = Globbing.Glob.Parse(pattern);
            var sut = new GlobMatchStringGenerator(glob.Tokens);

            for (int i = 0; i < volume; i++)
            {
                var generatedString = sut.GenerateRandomNonMatch();
                var result = glob.IsMatch(generatedString);
                Assert.False(result, string.Format("{0} matched pattern {1}", generatedString, pattern));
            }
        }

        [Theory]
        [InlineData("literal", 1000)]
        [InlineData("a/literal", 1000)]
        [InlineData("path/*atstand", 1000)]
        [InlineData("path/hats*nd", 1000)]
        [InlineData("path/?atstand", 1000)]
        [InlineData("path/?atstand?", 1000)]
        [InlineData("p?th/*a[bcd]", 1000)]
        [InlineData("p?th/*a[bcd]b[e-g]a[1-4]", 1000)]
        [InlineData("p?th/*a[bcd]b[e-g]a[1-4][!wxyz]", 1000)]
        [InlineData("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*", 1000)]
        [InlineData("path/**/somefile.txt", 1000)]
        public void Can_Generate_Matches(string pattern, int volume)
        {
            //  var generatedStrings = new List<string>(testStrings);
            var glob = Globbing.Glob.Parse(pattern);
            var sut = new GlobMatchStringGenerator(glob.Tokens);

            for (int i = 0; i < volume; i++)
            {
                var generatedString = sut.GenerateRandomMatch();
                var result = glob.IsMatch(generatedString);
                Assert.True(result, string.Format("{0} did not match pattern {1}", generatedString, pattern));
            }
        }

        //public void Can_Generate_Match(string pattern, int volume)
        //{
        //    //  var generatedStrings = new List<string>(testStrings);
        //    var glob = Glob.Parse(pattern);
        //    var sut = new GlobMatchStringGenerator(glob.Tokens);

        //    for (int i = 0; i < volume; i++)
        //    {
        //        var generatedString = sut.GenerateRandomMatch();
        //        var result = glob.IsMatch(generatedString);
        //        Assert.True(result, string.Format("{0} did not match pattern {1}", generatedString, pattern));
        //    }
        //}







    }
}
