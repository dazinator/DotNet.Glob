```

BenchmarkDotNet v0.14.0, Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 10.0.100
  [Host] : .NET 8.0.22 (8.0.2225.52707), X64 RyuJIT AVX2


```
| Method                 | NumberOfMatches | Pattern              | Mean | Error | Min | Max | Ratio | RatioSD | Alloc Ratio |
|----------------------- |---------------- |--------------------- |-----:|------:|----:|----:|------:|--------:|------------:|
| Compiled_Regex_IsMatch | 1               | p?th/(...)].txt [21] |   NA |    NA |  NA |  NA |     ? |       ? |           ? |

Benchmarks with issues:
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: DefaultJob [NumberOfMatches=1, Pattern=p?th/(...)].txt [21]]
