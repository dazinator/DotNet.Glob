# DotNet.Glob
A fast (probably the fastest) globbing library for .NET.

| Branch  | Build Status | NuGet |
| ------------- | ------------- | ----- |
| Master  |[![Build master](https://ci.appveyor.com/api/projects/status/yab1btvh7bvkkgva/branch/master?svg=true)](https://ci.appveyor.com/project/dazinator/dotnet-glob/branch/master) | [![NuGet](https://img.shields.io/nuget/v/DotNet.Glob.svg)](https://www.nuget.org/packages/DotNet.Glob/) |
| Develop | [![Build status](https://ci.appveyor.com/api/projects/status/yab1btvh7bvkkgva/branch/develop?svg=true)](https://ci.appveyor.com/project/dazinator/dotnet-glob/branch/develop)  | [![NuGet](https://img.shields.io/nuget/vpre/DotNet.Glob.svg)](https://www.nuget.org/packages/DotNet.Glob/) |

This library **does not** use Regex - I wanted to make something much faster.
The latest benchmarks show that DotNet.Glob signficantly outperforms Regex.
The benchmarks use [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) and can be located inside this repo. Just `dotnet run` them. Some Benchmark results have also been published on the wiki: https://github.com/dazinator/DotNet.Glob/wiki/Benchmarks-(vs-Compiled-Regex)

# Usage
1. Install the NuGet package. `Install-Package DotNet.Glob`
2. Add using statement:
   `using DotNet.Globbing;`
3. Parse a glob from a pattern
```
 var glob = Glob.Parse("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*");
 var isMatch = glob.IsMatch("pAth/fooooacbfa2vd4.txt");

```

# Build a glob fluently

You can also use the `GlobBuilder` class if you wish to build up a glob using a fluent syntax.
This is also more efficient as it avoids having to parse the glob from a string pattern.

So to build the following glob pattern: `/foo?\\*[abc][!1-3].txt`:

```csharp

  var glob = new GlobBuilder()
                .PathSeperator()
                .Literal("foo")
                .AnyCharacter()
                .PathSeperator(PathSeperatorKind.BackwardSlash)
                .Wildcard()
                .OneOf('a', 'b', 'c')
                .NumberNotInRange('1', '3')
                .Literal(".txt")
                .ToGlob();

   var isMatch = glob.IsMatch(@"/fooa\\barrra4.txt"); // returns true.

```


# Patterns

The following patterns are supported ([from wikipedia](https://en.wikipedia.org/wiki/Glob_(programming))):
> 
| Wildcard  | Description | Example | Matches | Does not match |
| --------  | ----------- | ------- | ------- | -------------- |
| \* |  matches any number of any characters including none	| Law\*| Law, Laws, or Lawyer	|
| ?	| matches any single character	| ?at	| Cat, cat, Bat or bat	| at |
| [abc] |	matches one character given in the bracket |	[CB]at |	Cat or Bat	| cat or bat |
| [a-z] |	matches one character from the range given in the bracket	| Letter[0-9]	| Letter0, Letter1, Letter2 up to Letter9	| Letters, Letter or Letter10 |
| [!abc] | matches one character that is not given in the bracket | [!C]at | Bat, bat, or cat | Cat |
| [!a-z] | matches one character that is not from the range given in the bracket | Letter[!3-5] | Letter1, Letter2, Letter6 up to Letter9 and Letterx etc. | Letter3, Letter4, Letter5 or Letterxx |

In addition, DotNet Glob also supports:

| Wildcard  | Description | Example | Matches | Does not match |
| --------  | ----------- | ------- | ------- | -------------- |
| `**` |  matches any number of path / directory segments. When used must be the only contents of a segment. | /\*\*/some.\* | /foo/bar/bah/some.txt, /some.txt, or /foo/some.txt	|


# Advanced Usages

## Parsing options.
By default, when your glob pattern is parsed, `DotNet.Glob` will only allow literals which are valid for path / directory names.
These are:

1. Any Letter (A-Z, a-z) or Digit
2. `.`, ` `, `!`, `#`, `-`, `;`, `=`, `@`, `~`, `_`, `:`

This is optimised for matching against paths / directory strings.
However, introduced in version `1.6.4`, you can override this behaviour so that you can include arbitrary characters in your literals. For example, here is a pattern that matches the literal `"Stuff`:

```csharp
    // Overide the default options globally for all matche:
    GlobOptions.Default.Parsing.AllowInvalidPathCharacters = true;
    DotNet.Globbing.Glob.Parse("\"Stuff*").IsMatch("\"Stuff"); // true;    
```

You can also just set these options on a per glob pattern basis:

```csharp
    GlobOptions options = new GlobOptions();
    options.Parsing.AllowInvalidPathCharacters = allowInvalidPathCharcters;
    DotNet.Globbing.Glob.Parse("\"Stuff*", options).IsMatch("\"Stuff"); // true; 

```

## Case Sensitivity (Available as of version >= 2.0.0)

By default, evaluation is case-sensitive unless you specify otherwise.

```csharp
    GlobOptions options = new GlobOptions();
    options.Evaluation.CaseInsensitive = true;
    DotNet.Globbing.Glob.Parse("foo*", options).IsMatch("FOo"); // true; 

```

Setting `CaseInsensitive` has an impact on:

- Letter Ranges. Any letter range (i.e '[A-Z]') will now match both lower or upper case characters.
- Character Lists. Any character list (i.e '[ABC]') will now match both lower or upper case characters.
- Literals. Any literal (i.e 'foo') will now match both lower or upper case characters i.e `FoO` will match `foO` etc.


## Match Generation
Given a glob, you can generate random matches, or non matches, for that glob.
For example, given the glob pattern `/f?o/bar/**/*.txt` you could generate matching strings like `/foo/bar/ajawd/awdaw/adw-ad.txt` or random non matching strings.


```
  var dotnetGlob = Glob.Parse(pattern);
  var generator = new GlobMatchStringGenerator(dotnetGlob.Tokens);

  for (int i = 0; i < 10; i++)
  {
          var testString = generator.GenerateRandomMatch();
          var result = dotnetGlob.IsMatch(testString);
          // result is always true.

          // generate a non match.
          testString = generator.GenerateRandomNonMatch();
          var result = dotnetGlob.IsMatch(testString);
           // result is always false.
  }

```
