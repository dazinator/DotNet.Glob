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
        [InlineData("C:\\THIS_IS_A_DIR\\*", "C:\\THIS_IS_A_DIR\\somefile")] // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/20
        [InlineData("/DIR1/*/*", "/DIR1/DIR2/file.txt")]  // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/21
        [InlineData("~/*~3", "~/abc123~3")]  // Regression Test for https://github.com/dazinator/DotNet.Glob/pull/15
        public void IsMatch(string pattern, params string[] testStrings)
        {
            var glob = Glob.Parse(pattern);
            foreach (var testString in testStrings)
            {
                var match = glob.IsMatch(testString);
                Assert.True(match);                
                           
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
        [InlineData("C:\\THIS_IS_A_DIR\\*", "C:\\THIS_IS_A_DIR\\somefile")] // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/20
        [InlineData("/DIR1/*/*", "/DIR1/DIR2/file.txt")]  // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/21
        [InlineData("~/*~3", "~/abc123~3")]  // Regression Test for https://github.com/dazinator/DotNet.Glob/pull/15  
        public void Match(string pattern, params string[] testStrings)
        {
            var glob = Glob.Parse(pattern);
            foreach (var testString in testStrings)
            {
                var match = glob.Match(testString);
                Assert.True(match.Success);
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


      


      
    }
}
