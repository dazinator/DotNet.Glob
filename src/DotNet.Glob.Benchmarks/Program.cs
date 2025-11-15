using System;
using System.Linq;
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
            // Create a config that uses InProcessEmitToolchain to avoid build/signing issues
            // Use Job.Dry for quick validation in CI, full benchmarking can be done locally
            var config = ManualConfig.Create(DefaultConfig.Instance)
                .AddExporter(HtmlExporter.Default)
                .AddExporter(CsvExporter.Default)
                .WithOptions(ConfigOptions.DisableOptimizationsValidator); // Disable optimizations validator for CI
            
            // Override any jobs to use InProcessEmitToolchain
            config = config.AddJob(Job.Dry.WithToolchain(new InProcessEmitToolchain(TimeSpan.FromHours(1), true)));

            // Use BenchmarkSwitcher to support running all benchmarks
            var switcher = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly);
            switcher.Run(args, config);
        }
    }
}
