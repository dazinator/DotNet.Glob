using Xunit;

namespace DotNet.Glob.Tests
{
    public class OtherGlobLibraryTests
    {

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
    }
}
