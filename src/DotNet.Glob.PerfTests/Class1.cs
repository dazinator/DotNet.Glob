using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using DotNet.Globbing;
using Glob;
using Xunit.Abstractions;

namespace DotNet.Glob.PerfTests
{
    public class GlobPerfTests
    {
        private ITestOutputHelper _output;

        public GlobPerfTests(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact(Skip = "Performance Tests Run Manually When Needed")]
        public void Glob_Matching_Performance()
        {
            // warmup each parser.
            var dotnetGlob = DotNet.Globbing.Glob.Parse("p?th/*a[bcd].*");
            var comparisonGlob = new global::Glob.Glob("p?th/*a[bcd].*");

            // create each parser from a pattern.
            var timer = new Stopwatch();

            timer.Restart();
            comparisonGlob = new global::Glob.Glob("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*", GlobOptions.Compiled);
            timer.Stop();
            _output.WriteLine(timer.Elapsed.ToString());

            timer.Restart();
            dotnetGlob = DotNet.Globbing.Glob.Parse("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*");
            timer.Stop();
            _output.WriteLine(timer.Elapsed.ToString());

            // match the same successful pattern over and over
            timer.Restart();
            for (int i = 0; i < 1000; i++)
            {
                var result = dotnetGlob.IsMatch("pAth/fooooacbfa2vd4.txt");
                Assert.True(result);
            }
            timer.Stop();
            _output.WriteLine(timer.Elapsed.ToString());


            // match the same successful pattern over and over
            timer.Restart();
            for (int i = 0; i < 1000; i++)
            {
                var result = comparisonGlob.IsMatch("pAth/fooooacbfa2vd4.txt");
                Assert.True(result);
            }
            timer.Stop();
            _output.WriteLine(timer.Elapsed.ToString());

        }
    }
 


}
