using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using DotNet.Globbing;
using DotNet.Globbing.Generation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes.Columns;

namespace DotNet.Glob.PerfTests
{

    [ClrJob, CoreJob, MemoryDiagnoser, MinColumn, MaxColumn]
    public class RegexGlobIsMatchBenchmarks : BaseGlobBenchMark
    {
        private const int MaxResults = 10000;
        private string _pattern;
        private Regex _compiledRegex;

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
                _compiledRegex = CreateRegex(_pattern, true);
                int half = MaxResults / 2;
                base.InitialiseGlobTestData(value, half, half);
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

    }
}
