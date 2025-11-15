``` ini

BenchmarkDotNet=v0.10.5, OS=ubuntu 24.04
Processor=AMD EPYC 7763 64-Core Processor, ProcessorCount=4
Frequency=1000000000 Hz, Resolution=1.0000 ns, Timer=UNKNOWN
dotnet cli version=10.0.100
  [Host] : .NET 8.0.22, 64bit RyuJIT


```
 |                  Method |  Job | Runtime |                                        Pattern | Mean | Error | Min | Max | Scaled | ScaledSD | Allocated |
 |------------------------ |----- |-------- |----------------------------------------------- |-----:|------:|----:|----:|-------:|---------:|----------:|
 | **New_Compiled_Regex_Glob** |  **Clr** |     **Clr** |                          **p?th/a[bcd]b[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |         New_DotNet_Glob |  Clr |     Clr |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | New_Compiled_Regex_Glob | Core |    Core |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |         New_DotNet_Glob | Core |    Core |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **New_Compiled_Regex_Glob** |  **Clr** |     **Clr** | **p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |         New_DotNet_Glob |  Clr |     Clr | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | New_Compiled_Regex_Glob | Core |    Core | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |         New_DotNet_Glob | Core |    Core | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **New_Compiled_Regex_Glob** |  **Clr** |     **Clr** |                                **p?th/a[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |         New_DotNet_Glob |  Clr |     Clr |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | New_Compiled_Regex_Glob | Core |    Core |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |         New_DotNet_Glob | Core |    Core |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |

Benchmarks with issues:
  BaselineRegexGlobCompileBenchmarks.New_Compiled_Regex_Glob: Clr(Runtime=Clr) [Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexGlobCompileBenchmarks.New_DotNet_Glob: Clr(Runtime=Clr) [Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexGlobCompileBenchmarks.New_Compiled_Regex_Glob: Core(Runtime=Core) [Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexGlobCompileBenchmarks.New_DotNet_Glob: Core(Runtime=Core) [Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexGlobCompileBenchmarks.New_Compiled_Regex_Glob: Clr(Runtime=Clr) [Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexGlobCompileBenchmarks.New_DotNet_Glob: Clr(Runtime=Clr) [Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexGlobCompileBenchmarks.New_Compiled_Regex_Glob: Core(Runtime=Core) [Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexGlobCompileBenchmarks.New_DotNet_Glob: Core(Runtime=Core) [Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexGlobCompileBenchmarks.New_Compiled_Regex_Glob: Clr(Runtime=Clr) [Pattern=p?th/a[e-g].txt]
  BaselineRegexGlobCompileBenchmarks.New_DotNet_Glob: Clr(Runtime=Clr) [Pattern=p?th/a[e-g].txt]
  BaselineRegexGlobCompileBenchmarks.New_Compiled_Regex_Glob: Core(Runtime=Core) [Pattern=p?th/a[e-g].txt]
  BaselineRegexGlobCompileBenchmarks.New_DotNet_Glob: Core(Runtime=Core) [Pattern=p?th/a[e-g].txt]
