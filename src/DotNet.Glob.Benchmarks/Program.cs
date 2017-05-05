using BenchmarkDotNet.Running;

namespace DotNet.Glob.Benchmarks
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
