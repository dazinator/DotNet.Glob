using DotNet.Globbing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotNet.Globbing.Tests
{
    public class GlobTests
    {

        /// <summary>
        /// Tests for the InMemoryDirectory sub system.
        /// </summary>
        [Theory]
        [InlineData("literal", "fliteral", "foo/literal", "literals", "literals/foo")]
        [InlineData("path/hats*nd", "path/hatsblahn", "path/hatsblahndt")]
        [InlineData("path/?atstand", "path/moatstand", "path/batstands")]
        public void Does_Not_Match(string pattern, params string[] testStrings)
        {
            var glob = Glob.Parse(pattern);
            foreach (var testString in testStrings)
            {
                Assert.False(glob.IsMatch(testString));
            }
        }


        /// <summary>
        /// Tests for the InMemoryDirectory sub system.
        /// </summary>
        [Theory]
        [InlineData("literal", "literal")]
        [InlineData("a/literal", "a/literal")]
        [InlineData("path/*atstand", "path/fooatstand")]
        [InlineData("path/hats*nd", "path/hatsforstand")]
        [InlineData("path/?atstand", "path/hatstand")]
        [InlineData("path/?atstand?", "path/hatstands")]
        [InlineData("p?th/*a[bcd]", "pAth/fooooac")]
        [InlineData("p?th/*a[bcd]b[e-g]a[1-4]", "pAth/fooooacbfa2")]
        [InlineData("p?th/*a[bcd]b[e-g]a[1-4][!wxyz]", "pAth/fooooacbfa2v")]
        [InlineData("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*", "pAth/fooooacbfa2vd4.txt")]
        public void Can_Match(string pattern, params string[] testStrings)
        {
            var glob = Glob.Parse(pattern);
            foreach (var testString in testStrings)
            {
                var match = glob.Match(testString);

                Assert.True(match.Success);
            }
        }

    }
}
