using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet.Glob.PerfTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<BaselineRegexGlobCompileBenchmarks>();
            BenchmarkRunner.Run<BaselineRegexIsMatchTrueBenchmarks>();
            BenchmarkRunner.Run<BaselineRegexIsMatchFalseBenchmarks>();
            //BenchmarkRunner.Run<GlobBenchmarks>();
        }
    }
}
