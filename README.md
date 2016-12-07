# DotNet.Glob
A fast globbing library for .NET applications.


| Branch  | Build Status | NuGet |
| ------------- | ------------- | ----- |
| Master  |[![Build master](https://ci.appveyor.com/api/projects/status/yab1btvh7bvkkgva/branch/master?svg=true)](https://ci.appveyor.com/project/dazinator/dotnet-glob/branch/master) | [![NuGet](https://img.shields.io/nuget/v/DotNet-Glob.svg)](https://www.nuget.org/packages/DotNet.Glob/) |

# Usage

1. Install the NuGet package. "DotNet.Glob"
2.
```
using DotNet.Globbing;
```
3.
```
 var glob = Glob.Parse("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*");
 var isMatch = glob.IsMatch("pAth/fooooacbfa2vd4.txt");

```

The `IsMatch` method just returns you a boolean. If you require more in-depth information about the match including the tokens and values that were or weren't matched, you can do this:

```
 MatchInfo match = glob.Match(somestring);

```

Then you can inspect the `MatchInfo` which will have that useful context.

