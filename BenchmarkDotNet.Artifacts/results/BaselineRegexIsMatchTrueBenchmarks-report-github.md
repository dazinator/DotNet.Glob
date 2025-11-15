``` ini

BenchmarkDotNet=v0.10.5, OS=ubuntu 24.04
Processor=AMD EPYC 7763 64-Core Processor, ProcessorCount=4
Frequency=1000000000 Hz, Resolution=1.0000 ns, Timer=UNKNOWN
dotnet cli version=10.0.100
  [Host] : .NET 8.0.22, 64bit RyuJIT


```
 |                 Method |  Job | Runtime | NumberOfMatches |                                        Pattern | Mean | Error | Min | Max | Scaled | ScaledSD | Allocated |
 |----------------------- |----- |-------- |---------------- |----------------------------------------------- |-----:|------:|----:|----:|-------:|---------:|----------:|
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |               **1** |                          **p?th/a[bcd]b[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |               1 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |               1 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |               1 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |               **1** | **p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |               1 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |               1 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |               1 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |               **1** |                                **p?th/a[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |               1 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |               1 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |               1 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |              **10** |                          **p?th/a[bcd]b[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |              10 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |              10 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |              10 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |              **10** | **p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |              10 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |              10 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |              10 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |              **10** |                                **p?th/a[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |              10 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |              10 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |              10 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |             **100** |                          **p?th/a[bcd]b[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |             100 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |             100 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |             100 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |             **100** | **p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |             100 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |             100 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |             100 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |             **100** |                                **p?th/a[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |             100 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |             100 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |             100 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |             **500** |                          **p?th/a[bcd]b[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |             500 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |             500 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |             500 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |             **500** | **p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |             500 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |             500 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |             500 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |             **500** |                                **p?th/a[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |             500 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |             500 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |             500 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |            **1000** |                          **p?th/a[bcd]b[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |            1000 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |            1000 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |            1000 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |            **1000** | **p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |            1000 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |            1000 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |            1000 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |            **1000** |                                **p?th/a[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |            1000 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |            1000 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |            1000 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |           **10000** |                          **p?th/a[bcd]b[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |           10000 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |           10000 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |           10000 |                          p?th/a[bcd]b[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |           **10000** | **p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |           10000 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |           10000 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |           10000 | p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | **Compiled_Regex_IsMatch** |  **Clr** |     **Clr** |           **10000** |                                **p?th/a[e-g].txt** |   **NA** |    **NA** |  **NA** |  **NA** |      **?** |        **?** |       **N/A** |
 |     DotNetGlob_IsMatch |  Clr |     Clr |           10000 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 | Compiled_Regex_IsMatch | Core |    Core |           10000 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |
 |     DotNetGlob_IsMatch | Core |    Core |           10000 |                                p?th/a[e-g].txt |   NA |    NA |  NA |  NA |      ? |        ? |       N/A |

Benchmarks with issues:
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=1, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=1, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=1, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=1, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=1, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=1, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=1, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=1, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=1, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=1, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=1, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=1, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=10, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=10, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=10, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=10, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=10, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=10, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=10, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=10, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=10, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=10, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=10, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=10, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=100, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=100, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=100, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=100, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=100, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=100, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=100, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=100, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=100, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=100, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=100, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=100, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=500, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=500, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=500, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=500, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=500, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=500, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=500, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=500, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=500, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=500, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=500, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=500, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=1000, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=1000, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=1000, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=1000, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=1000, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=1000, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=1000, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=1000, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=1000, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=1000, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=1000, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=1000, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=10000, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=10000, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=10000, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=10000, Pattern=p?th/a[bcd]b[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=10000, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=10000, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=10000, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=10000, Pattern=p?th/a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=10000, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Clr(Runtime=Clr) [NumberOfMatches=10000, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.Compiled_Regex_IsMatch: Core(Runtime=Core) [NumberOfMatches=10000, Pattern=p?th/a[e-g].txt]
  BaselineRegexIsMatchTrueBenchmarks.DotNetGlob_IsMatch: Core(Runtime=Core) [NumberOfMatches=10000, Pattern=p?th/a[e-g].txt]
