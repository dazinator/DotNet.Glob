using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Jobs;

namespace DotNet.Glob.Benchmarks
{

    [ClrJob, CoreJob, MemoryDiagnoser, MinColumn, MaxColumn]
    public class BaselineRegexIsMatchTrueBenchmarks : BaseGlobBenchMark
    {
        private const int MaxResults = 10000;

        private string _pattern;
       // private global::Glob.Glob _glob;
        private Regex _compiledRegex;
        private Globbing.Glob _dotnetGlob;


        [Params(1, 10, 100, 500, 1000, MaxResults)]
        public int NumberOfMatches { get; set; }

        [Params("p?th/a[e-g].txt",
                "p?th/a[bcd]b[e-g].txt",
                "p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt")]
        public override string Pattern
        {
            get
            {
                return _pattern;
            }
            set
            {
                _pattern = value;
               // _glob = new global::Glob.Glob(_pattern);
                _dotnetGlob = Globbing.Glob.Parse(_pattern);
                _compiledRegex = CreateRegex(_dotnetGlob.Tokens, true);
                base.InitialiseGlobTestData(value, MaxResults, 0);
            }
        }

        [Benchmark(Baseline = true)]
        public bool Compiled_Regex_IsMatch()
        {
            bool result = false;
            for (int i = 0; i < NumberOfMatches; i++)
            {
                var testString = TestStrings[i];
                result ^= _compiledRegex.IsMatch(testString);
            }
            return result;
        }

        //[Benchmark()]
        //public bool Glob_IsMatch()
        //{
        //    bool result = false;
        //    for (int i = 0; i < NumberOfMatches; i++)
        //    {
        //        var testString = TestStrings[i];
        //        result ^= _glob.IsMatch(testString);
        //    }
        //    return result;
        //}

        [Benchmark()]
        public bool DotNetGlob_IsMatch()
        {
            bool result = false;
            for (int i = 0; i < NumberOfMatches; i++)
            {
                var testString = TestStrings[i];
                result ^= _dotnetGlob.IsMatch(testString);

            }
            return result;
        }

    }
}
