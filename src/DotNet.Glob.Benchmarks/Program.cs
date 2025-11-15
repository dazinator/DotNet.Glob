using System;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace DotNet.Glob.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                .AddExporter(HtmlExporter.Default)
                .AddExporter(CsvExporter.Default)
                .AddJob(Job.Default
                    .WithToolchain(new InProcessEmitToolchain(TimeSpan.FromHours(1), true)));

            BenchmarkRunner.Run<BaselineRegexGlobCompileBenchmarks>(config);
            BenchmarkRunner.Run<BaselineRegexIsMatchTrueBenchmarks>(config);
            BenchmarkRunner.Run<BaselineRegexIsMatchFalseBenchmarks>(config);
            //BenchmarkRunner.Run<GlobBenchmarks>();
        }
    }
}
