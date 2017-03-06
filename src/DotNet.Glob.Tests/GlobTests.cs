using DotNet.Globbing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.DotNet.ProjectModel;
using Xunit;

namespace DotNet.Globbing.Tests
{
    public class GlobTests
    {
       
        [Theory]
        [InlineData("literal", "fliteral", "foo/literal", "literals", "literals/foo")]
        [InlineData("path/hats*nd", "path/hatsblahn", "path/hatsblahndt")]
        [InlineData("path/?atstand", "path/moatstand", "path/batstands")]
        [InlineData("/**/file.csv", "/file.txt")]
        [InlineData("/*file.txt", "/folder")]
        public void Does_Not_Match(string pattern, params string[] testStrings)
        {
            var glob = Glob.Parse(pattern);
            foreach (var testString in testStrings)
            {
                Assert.False(glob.IsMatch(testString));
            }
        }
    
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
        [InlineData("path/**/somefile.txt", "path/foo/bar/baz/somefile.txt")]
        [InlineData("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*", "pGth/yGKNY6acbea3rm8.")]
        [InlineData("/**/file.*", "/folder/file.csv")]
        [InlineData("/**/file.*", "/file.txt")]
        [InlineData("/**/file.*", "/file.txt")]
        [InlineData("**/file.*", "/file.txt")]
        [InlineData("/*file.txt", "/file.txt")]
        public void Can_IsMatch(string pattern, params string[] testStrings)
        {
            var glob = Glob.Parse(pattern);
            foreach (var testString in testStrings)
            {
                var match = glob.IsMatch(testString);
                Assert.True(match);
            }
        }
        
        [Fact]
        public void To_String_Returns_Pattern()
        {
            var pattern = "p?th/*a[bcd]b[e-g]/**/a[1-4][!wxyz][!a-c][!1-3].*";
            var glob = Glob.Parse(pattern);
            var resultPattern = glob.ToString();
            Assert.Equal(pattern, resultPattern);
        }
    
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
        [InlineData("path/**/somefile.txt", "path/foo/bar/baz/somefile.txt")]
        [InlineData("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*", "pGth/yGKNY6acbea3rm8.")]
        [InlineData("/**/file.*", "/folder/file.csv")]
        //[InlineData("/**/file.*", "/file.txt")]
        //[InlineData("**/file.*", "/file.txt")]
        //[InlineData("/**/file.*", "/file.txt")]
        //[InlineData("/**/f~le.*", "/f~le.txt")]
        public void Glob_IsMatch(string pattern, params string[] testStrings)
        {
            // This is a different glob library, I am seeing if it matches the same patterns as my library.
            // The tests above commented out show it has some limitations, that I have addressed in this library.
            var glob = new global::Glob.Glob(pattern);
            foreach (var testString in testStrings)
            {
                var match = glob.IsMatch(testString);
                Assert.True(match);
            }
        }


        /// <summary>
        /// Regression Test for https://github.com/dazinator/DotNet.Glob/pull/15
        /// </summary>
        [Fact]
        public void BugFix_Can_Read_Tilde()
        {
            // This is a different glob library, I am seeing if it matches the same patterns as my library.
            // The three tests above commented out show it currently has some limitations, that this library doesn't.
            var glob = Glob.Parse("~/*~3");
            var isMatch = glob.IsMatch("~/abc123~3");
            Assert.True(isMatch);
        }

        /// <summary>
        /// Regression Test for https://github.com/dazinator/DotNet.Glob/pull/15
        /// </summary>
        [Fact]
        public void BugFix_Issue_20_Can_Read_Colon_And_Underscore()
        {
            // This is a different glob library, I am seeing if it matches the same patterns as my library.
            // The three tests above commented out show it currently has some limitations, that this library doesn't.
            var glob = Glob.Parse("C:\\THIS_IS_A_DIR\\*");
            var isMatch = glob.IsMatch("C:\\THIS_IS_A_DIR\\somefile");
            Assert.True(isMatch);
        }
    }
}
