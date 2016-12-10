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
            BenchmarkRunner.Run<DotNetGlobBenchmarks>();
            BenchmarkRunner.Run<GlobBenchmarks>();
        }
    }
}
