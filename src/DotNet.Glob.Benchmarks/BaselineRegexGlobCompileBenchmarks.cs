using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using DotNet.Globbing;
using DotNet.Globbing.Generation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Glob;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes.Columns;
using System.Text.RegularExpressions;

namespace DotNet.Glob.PerfTests
{

    [ClrJob, CoreJob, MemoryDiagnoser, MinColumn, MaxColumn]
    public class BaselineRegexGlobCompileBenchmarks : BaseGlobBenchMark
    {
        private Globbing.Glob _dotnetGlob;
        private Regex _compiledRegex;

        private string _pattern;
        private string _regexString;
      

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
                var tokens = new GlobTokeniser().Tokenise(_pattern);
                _regexString = new GlobToRegexFormatter().Format(tokens);
            }
        }

        [Benchmark(Baseline = true)]
        public Regex New_Compiled_Regex_Glob()
        {
            var result = new Regex(_regexString, RegexOptions.Compiled | RegexOptions.Singleline);
            return result;
        }

        [Benchmark()]
        public Globbing.Glob New_DotNet_Glob()
        {
            var result = Globbing.Glob.Parse(Pattern);
            return result;
        }

        //[Benchmark()]
        //public global::Glob.Glob New_Glob()
        //{
        //    var result = new global::Glob.Glob(Pattern);
        //    return result;

        //}


    }
}
