using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using DotNet.Globbing;
using DotNet.Globbing.Generation;

namespace DotNet.Glob.Benchmarks
{

    [ClrJob, CoreJob, MemoryDiagnoser, MarkdownExporter, MinColumn, MaxColumn]
    public class DotNetGlobBenchmarks
    {

        private Globbing.Glob _glob;

        private List<string> _testMatchingStringsList;
        private List<string> _testNonMatchingStringsList;

        [Setup]
        public void SetupData()
        {
            _testMatchingStringsList = new List<string>(NumberOfMatches);
            _testNonMatchingStringsList = new List<string>(NumberOfMatches);
            _glob = Globbing.Glob.Parse(GlobPattern);
            var generator = new GlobMatchStringGenerator(_glob.Tokens);

            for (int i = 0; i < 10000; i++)
            {
                _testMatchingStringsList.Add(generator.GenerateRandomMatch());
                _testNonMatchingStringsList.Add(generator.GenerateRandomNonMatch());
            }

        }

        [Params(1, 10, 100, 200, 500, 1000, 10000)]
        public int NumberOfMatches { get; set; }

        [Params("p?th/a[e-g].txt",
                "p?th/a[bcd]b[e-g].txt",
                "p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt")]
        public string GlobPattern { get; set; }

        [Benchmark]
        public bool IsMatch_True()
        {
            // we collect all results in a list and return it to prevent dead code elimination (optimisation)
            var result = false;
            for (int i = 0; i < NumberOfMatches; i++)
            {
                var testString = _testMatchingStringsList[i];
                result ^= _glob.IsMatch(testString);
            }
            return result;
        }

        [Benchmark]
        public bool IsMatch_False()
        {
            var result = false;
            for (int i = 0; i < NumberOfMatches; i++)
            {
                var testString = _testNonMatchingStringsList[i];
                result ^= _glob.IsMatch(testString);
            }
            return result;
        }

    }
}
