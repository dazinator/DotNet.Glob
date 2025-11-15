using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;

namespace DotNet.Glob.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                .With(HtmlExporter.Default)
                .With(CsvExporter.Default);

            BenchmarkRunner.Run<BaselineRegexGlobCompileBenchmarks>(config);
            BenchmarkRunner.Run<BaselineRegexIsMatchTrueBenchmarks>(config);
            BenchmarkRunner.Run<BaselineRegexIsMatchFalseBenchmarks>(config);
            //BenchmarkRunner.Run<GlobBenchmarks>();
        }
    }
}
