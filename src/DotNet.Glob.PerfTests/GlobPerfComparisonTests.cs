using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Glob;
using Xunit.Abstractions;
using DotNet.Globbing.Generation;

namespace DotNet.Glob.PerfTests
{
    public class GlobPerfComparisonTests
    {
        private ITestOutputHelper _output;

        public GlobPerfComparisonTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        //[Fact()]
        //public void Performs_Faster_Than_Another_Glob_Library()
        //{
        //    // warmup each parser.
        //    var dotnetGlob = DotNet.Globbing.Glob.Parse("p?th/*a[bcd].*");
        //    var comparisonGlob = new global::Glob.Glob("p?th/*a[bcd].*");

        //    // create each parser from a pattern.
        //    var timer = new Stopwatch();
        //    TimeSpan comparisonTime;
        //    timer.Restart();
        //    comparisonGlob = new global::Glob.Glob("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*", GlobOptions.Compiled);
        //    timer.Stop();
        //    _output.WriteLine(timer.Elapsed.ToString());
        //    comparisonTime = timer.Elapsed;


        //    TimeSpan thisTime;
        //    timer.Restart();
        //    dotnetGlob = DotNet.Globbing.Glob.Parse("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*");
        //    timer.Stop();
        //    _output.WriteLine(timer.Elapsed.ToString());
        //    thisTime = timer.Elapsed;

        //    // Fail if we are ever slower.
        //    // Assert.True(thisTime < comparisonTime);

        //    // warm up 10 matches.

        //    var generator = new GlobMatchStringGenerator(dotnetGlob.Tokens);

        //    for (int i = 0; i < 10; i++)
        //    {
        //        var testString = generator.GenerateRandomMatch();
        //        var result = dotnetGlob.IsMatch(testString);
        //        result = comparisonGlob.IsMatch(testString);
        //    }

        //    // perform a 1000 matches.
        //    // generate 1000 random matching strings.
        //    var testStrings = new List<string>();
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        testStrings.Add(generator.GenerateRandomMatch());
        //    }

        //    Console.WriteLine("Memory used before collection:       {0:N0}", GC.GetTotalMemory(false));
        //    GC.Collect();
        //    Console.WriteLine("Memory used after full collection:   {0:N0}", GC.GetTotalMemory(true));

        //    timer.Restart();
        //    foreach (var testString in testStrings)
        //    {
        //        var result = dotnetGlob.IsMatch(testString);
        //        //if (!result)
        //        //{
        //        //    Debugger.Break();
        //        //}
        //        Assert.True(result);
        //    }
        //    timer.Stop();
        //    _output.WriteLine(timer.Elapsed.ToString());
        //    thisTime = timer.Elapsed;

        //    // match the same successful pattern over and over
        //    timer.Restart();
        //    foreach (var testString in testStrings)
        //    {
        //        var result = comparisonGlob.IsMatch(testString);
        //        //if (!result)
        //        //{
        //        //    Debugger.Break();
        //        //}
        //        Assert.True(result);
        //    }
        //    timer.Stop();
        //    _output.WriteLine(timer.Elapsed.ToString());
        //    comparisonTime = timer.Elapsed;

        //    // Fail if we are ever slower.
        //    Assert.True(thisTime < comparisonTime);

        //}

       // [Theory(Skip = "Needs work")]
        [Theory()]
        [InlineData("p?th/a[e-g].txt", 100)]
        [InlineData("p?th/a[bcd]b[e-g].txt", 100)]
        [InlineData("p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt", 500)]
        [InlineData("p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt", 10000)]
        public void Performs_Faster_Than_Another_Library(string pattern, int total)
        {
            // warmup each parser.
            var dotnetGlob = DotNet.Globbing.Glob.Parse("p?th/*a[bcd].*");
            var comparisonGlob = new global::Glob.Glob("p?th/*a[bcd].*");

            // create each parser from a pattern.
            var timer = new Stopwatch();
            TimeSpan comparisonTime;
            timer.Restart();
            comparisonGlob = new global::Glob.Glob(pattern, GlobOptions.Compiled);
            timer.Stop();
            _output.WriteLine(timer.Elapsed.ToString());
            comparisonTime = timer.Elapsed;

            TimeSpan thisTime;
            timer.Restart();
            dotnetGlob = DotNet.Globbing.Glob.Parse(pattern);
            timer.Stop();
            _output.WriteLine(timer.Elapsed.ToString());
            thisTime = timer.Elapsed;

            // Fail if we are ever slower.
            // Assert.True(thisTime < comparisonTime);

            // warm up 10 matches.

            var generator = new GlobMatchStringGenerator(dotnetGlob.Tokens);

            for (int i = 0; i < 10; i++)
            {
                var testString = generator.GenerateRandomMatch();
                var result = dotnetGlob.IsMatch(testString);
                result = comparisonGlob.IsMatch(testString);
            }

            // perform a 1000 matches.
            // generate 1000 random matching strings.
            var testStrings = new List<string>();
            int totalNumber = total;
            for (int i = 0; i < totalNumber; i++)
            {
                testStrings.Add(generator.GenerateRandomMatch());
            }

            //_output.WriteLine("Memory used before collection:       {0:N0}", GC.GetTotalMemory(false));
            //GC.Collect();
            //_output.WriteLine("Memory used after full collection:   {0:N0}", GC.GetTotalMemory(true));

            timer.Restart();
            foreach (var testString in testStrings)
            {
                var result = dotnetGlob.IsMatch(testString);
                if (!result)
                {
                    Debugger.Break();
                }
                Assert.True(result);
            }
            timer.Stop();
            _output.WriteLine("dotnetGlob " + totalNumber + " matches took:" + timer.Elapsed.ToString());
            thisTime = timer.Elapsed;

            // match the same successful pattern over and over
            timer.Restart();
            foreach (var testString in testStrings)
            {
                var result = comparisonGlob.IsMatch(testString);
                if (!result)
                {
                    Debugger.Break();
                }
                Assert.True(result);
            }
            timer.Stop();
            _output.WriteLine("comparisonGlob " + totalNumber + " matches took:" + timer.Elapsed.ToString());
            comparisonTime = timer.Elapsed;

            // Fail if we are ever slower.
            Assert.True(thisTime < comparisonTime, "comparison library was better by " + (thisTime - comparisonTime));

        }
    }



}
