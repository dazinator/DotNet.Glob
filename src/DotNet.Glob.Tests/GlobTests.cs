using System;
using DotNet.Globbing;
using Xunit;

namespace DotNet.Glob.Tests
{
    public class GlobTests
    {
        [Theory]
        [InlineData("literal", "fliteral", "foo/literal", "literals", "literals/foo")]
        [InlineData("path/hats*nd", "path/hatsblahn", "path/hatsblahndt")]
        [InlineData("path/?atstand", "path/moatstand", "path/batstands")]
        [InlineData("/**/file.csv", "/file.txt")]
        [InlineData("/*file.txt", "/folder")]
        [InlineData("Shock* 12", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Adobe\\Shockwave 12")]
        [InlineData("*Shock* 12", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Adobe\\Shockwave 12")]
        [InlineData("*ave*2", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Adobe\\Shockwave 12")]
        [InlineData("*ave 12", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Adobe\\Shockwave 12")]
        [InlineData("*ave 12", "wave 12/")]
        [InlineData("C:\\THIS_IS_A_DIR\\**\\somefile.txt", "C:\\THIS_IS_A_DIR\\awesomefile.txt")] // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/27
        [InlineData("C:\\name\\**", "C:\\name.ext", "C:\\name_longer.ext")] // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/29
        [InlineData("Bumpy/**/AssemblyInfo.cs", "Bumpy.Test/Properties/AssemblyInfo.cs")]      // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/33
        [InlineData("C:\\sources\\x-y 1\\BIN\\DEBUG\\COMPILE\\**\\MSVC*120.DLL", "C:\\sources\\x-y 1\\BIN\\DEBUG\\COMPILE\\ANTLR3.RUNTIME.DLL")]      // Attempted repro for https://github.com/dazinator/DotNet.Glob/issues/37
        [InlineData("literal1", "LITERAL1")] // Regression tests for https://github.com/dazinator/DotNet.Glob/issues/41
        [InlineData("*ral*", "LITERAL1")] // Regression tests for https://github.com/dazinator/DotNet.Glob/issues/41
        [InlineData("[list]s", "LS", "iS", "Is")] // Regression tests for https://github.com/dazinator/DotNet.Glob/issues/41
        [InlineData("range/[a-b][C-D]", "range/ac", "range/Ad", "range/BD")] // Regression tests for https://github.com/dazinator/DotNet.Glob/issues/41
        [InlineData(@"abc/**", @"abcd")]
        [InlineData(@"**\segment1\**\segment2\**", @"C:\test\segment1\src\segment2")]
        [InlineData(@"**/.*", "foobar.")] // Regression test for https://github.com/dazinator/DotNet.Glob/issues/78  
        [InlineData(@"**/~*", "/")] // Regression test for https://github.com/dazinator/DotNet.Glob/issues/82
        public void Does_Not_Match(string pattern, params string[] testStrings)
        {
            var glob = Globbing.Glob.Parse(pattern);
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
        [InlineData("**/file.*", "/file.txt")]
        [InlineData("/*file.txt", "/file.txt")]
        [InlineData("C:\\THIS_IS_A_DIR\\*", "C:\\THIS_IS_A_DIR\\somefile")] // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/20
        [InlineData("/DIR1/*/*", "/DIR1/DIR2/file.txt")]  // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/21
        [InlineData("~/*~3", "~/abc123~3")]  // Regression Test for https://github.com/dazinator/DotNet.Glob/pull/15
        [InlineData("**\\Shock* 12", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Adobe\\Shockwave 12")]
        [InlineData("**\\*ave*2", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Adobe\\Shockwave 12")]
        [InlineData("**", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Adobe\\Shockwave 12")]
        [InlineData("**", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Adobe\\Shockwave 12.txt")]
        [InlineData("Stuff, *", "Stuff, x")]      // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/31
        [InlineData("\"Stuff*", "\"Stuff")]      // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/32
        [InlineData("path/**/somefile.txt", "path//somefile.txt")]
        [InlineData("**/app*.js", "dist/app.js", "dist/app.a72ka8234.js")]      // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/34
        [InlineData("**/y", "y")]      // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/44      
        [InlineData("**/gfx/*.gfx", "HKEY_LOCAL_MACHINE\\gfx\\foo.gfx", "HKEY_LOCAL_MACHINE/gfx/foo.gfx")]      // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/46   -  seems to work fine on mixed slashes.   
        [InlineData("**/gfx/**/*.gfx", "a_b\\gfx\\bar\\foo.gfx", "a_b/gfx/bar/foo.gfx")]      // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/46   - only seems to work on paths with forward slashes.
        [InlineData("**\\gfx\\**\\*.gfx", "a_b\\gfx\\bar\\foo.gfx", "a_b/gfx/bar/foo.gfx")]      // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/46    -  only seems to work on paths with backwards slashes.
        [InlineData(@"/foo/bar!.baz", @"/foo/bar!.baz")] // match a ! after bar
        [InlineData(@"/foo/bar[!!].baz", @"/foo/bar7.baz")] // anything except an exclaimation mark after bar
        [InlineData(@"/foo/bar[!]].baz", @"/foo/bar9.baz")] // anything except an ] after bar
        [InlineData(@"/foo/bar[!?].baz", @"/foo/bar7.baz")] // anything except an ? after bar
        [InlineData(@"/foo/bar[![].baz", @"/foo/bar7.baz")] // anything except an [ after bar
        [InlineData(@"C:\myergen\[[]a]tor", @"C:\myergen\[a]tor")]
        [InlineData(@"C:\myergen\[[]ator", @"C:\myergen\[ator")]
        [InlineData(@"C:\myergen\[[][]]ator", @"C:\myergen\[]ator")]
        [InlineData(@"C:\myergen[*]ator", @"C:\myergen*ator")]
        [InlineData(@"C:\myergen[*][]]ator", @"C:\myergen*]ator")]
        [InlineData(@"C:\myergen[*]]ator", @"C:\myergen*ator", @"C:\myergen]ator")]
        [InlineData(@"C:\myergen[?]ator", @"C:\myergen?ator")]
        [InlineData(@"/path[\]hatstand", @"/path\hatstand")]
        [InlineData(@"**\[#!]*\**", @"#test3", @"#test3\", @"\#test3\foo", @"\#test3")]
        [InlineData(@"**\[#!]*", @"#test3", "#this is a comment", @"\#test3")]
        [InlineData(@"[#!]*\**", "#this is a comment")]
        [InlineData(@"[#!]*", @"#test3", "#this is a comment")]
        [InlineData(@"abc/**", @"abc/def/hij.txt")]
        [InlineData(@"a/**/b", "a/b")] // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/65
        [InlineData(@"abc/**", "abc/def")] // Regression Test for https://github.com/dazinator/DotNet.Glob/issues/65
        public void IsMatch(string pattern, params string[] testStrings)
        {
            var glob = Globbing.Glob.Parse(pattern);
            foreach (var testString in testStrings)
            {
                var match = glob.IsMatch(testString);
                Assert.True(match, $"glob {pattern} failed to match test string: {testString}");
            }
        }

        // Regression tests for https://github.com/dazinator/DotNet.Glob/issues/41
        [Theory]
        [InlineData("literal1", "LITERAL1", "literal1")]
        [InlineData("*ral*", "LITERAL1", "literal1")]
        [InlineData("[list]s", "LS", "ls", "iS", "Is")]
        [InlineData("range/[a-b][C-D]", "range/ac", "range/Ad", "range/bC", "range/BD")]
        public void IsMatchCaseInsensitive(string pattern, params string[] testStrings)
        {
            var options = new GlobOptions();
            options.Evaluation.CaseInsensitive = true;

            var glob = Globbing.Glob.Parse(pattern, options);
            foreach (var testString in testStrings)
            {
                var match = glob.IsMatch(testString);
                Assert.True(match);
            }
        }

        [Fact]
        public void To_String_Returns_Pattern()
        {
            const string pattern = "p?th/*a[bcd]b[e-g]/**/a[1-4][!wxyz][!a-c][!1-3].*";
            var glob = Globbing.Glob.Parse(pattern);
            var resultPattern = glob.ToString();
            Assert.Equal(pattern, resultPattern);
        }

#if SPAN
        [Fact]
        public void Can_Use_ReadOnly_Span()
        {
            const string pattern = "p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*";
            var glob = Globbing.Glob.Parse(pattern);

            var span = "pAth/fooooacbfa2vd4.txt".AsSpan();
            Assert.True(glob.IsMatch(span));
        }
#endif
        //[Theory]

        //public void Can_Escape_Special_Characters(string pattern, string expectedFormatted)
        //{
        //    var glob = Globbing.Glob.Parse(pattern);
        //    Assert.Equal(expectedFormatted, glob.ToString());
        //}
    }
}
