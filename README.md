# DotNet.Glob
A fast (probably the fastest) globbing library for .NET.

![financial contributions](https://opencollective.com/dotnetglob/tiers/badge.svg) - [If you would like to say thanks](https://opencollective.com/dotnetglob#category-CONTRIBUTE)

| Branch  | Build Status | NuGet |
| ------------- | ------------- | ----- |
| Master  |[![Build master](https://ci.appveyor.com/api/projects/status/yab1btvh7bvkkgva/branch/master?svg=true)](https://ci.appveyor.com/project/dazinator/dotnet-glob/branch/master) | [![NuGet](https://img.shields.io/nuget/v/DotNet.Glob.svg)](https://www.nuget.org/packages/DotNet.Glob/) |
| Develop | [![Build status](https://ci.appveyor.com/api/projects/status/yab1btvh7bvkkgva/branch/develop?svg=true)](https://ci.appveyor.com/project/dazinator/dotnet-glob/branch/develop)  | [![NuGet](https://img.shields.io/nuget/vpre/DotNet.Glob.svg)](https://www.nuget.org/packages/DotNet.Glob/) |

This library **does not** use Regex - I wanted to make something faster.
The latest benchmarks show that `DotNet.Glob` outperforms Regex - that was my goal for this library.
The benchmarks use [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) and can be located inside this repo. Just `dotnet run` them. Some Benchmark results have also been published on the wiki: https://github.com/dazinator/DotNet.Glob/wiki/Benchmarks-(vs-Compiled-Regex)

# Usage
1. Install the NuGet package. `Install-Package DotNet.Glob`
2. Add using statement:
   `using DotNet.Globbing;`
3. Parse a glob from a pattern
```
 var glob = Glob.Parse("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*");
 var isMatch = glob.IsMatch("pAth/fooooacbfa2vd4.txt"); // You can also use ReadOnlySpan<char> on supported platforms.

```

# Build a glob fluently

You can also use the `GlobBuilder` class if you wish to build up a glob using a fluent syntax.
This is also more efficient as it avoids having to parse the glob from a string pattern.

So to build the following glob pattern: `/foo?\\*[abc][!1-3].txt`:

```csharp

  var glob = new GlobBuilder()
                .PathSeparator()
                .Literal("foo")
                .AnyCharacter()
                .PathSeparator(PathSeparatorKind.BackwardSlash)
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


# Escaping special characters

Wrap special characters `?, *, [` in square brackets in order to escape them.
You can also use negation when doing this.

Here are some examples:

| Pattern  | Description | Matches |  
| --------  | ----------- | ------- | 
|`/foo/bar[[].baz` | match a `[` after bar | `/foo/bar[.baz` |
|`/foo/bar[!!].baz` | match any character except `!` after bar | `/foo/bar7.baz` |
|`/foo/bar[!]].baz` | match any character except an ] after bar | `/foo/bar7.baz` |
|`/foo/bar[?].baz` | match an `?` after bar | `/foo/bar?.baz` |
|`/foo/bar[*]].baz` | match either a `*` or a `]` after bar | `/foo/bar*.baz`,`/foo/bar].baz` |
|`/foo/bar[*][]].baz` | match `*]` after bar | `/foo/bar*].baz` |

## ReadOnlySpan<char>

`ReadOnlySpan<char>` is supported as of version `3.0.0` of this library. 
You can read more about `Span` here: https://msdn.microsoft.com/en-us/magazine/mt814808.aspx

You must be targeting a platform that supports `ReadOnlySpan<T>` for this API to become available. These are currently:

- `.NET Core 2.1` 
- Platforms that implement `.NET Standard 2.1`

Usage remains very similar, except you can use the overload that takes a `ReadOnlySpan<char>` as opposed to a `string`:
```
    var glob = Globbing.Glob.Parse("p?th/*a[bcd]b[e-g]a[1-4][!wxyz][!a-c][!1-3].*");
    var span = "pAth/fooooacbfa2vd4.txt".AsSpan();
    Assert.True(glob.IsMatch(span));

```

There should be some performance benefits in utilising this in conjunction with other `Span` based API's being added to the .net framework / .net standard.

# Advanced Usages

## Options.

`DotNet.Glob` allows you to set options at a global level, however you can also override these options on a per glob basis, by passing in your own `GlobOptions` instance to a glob.

To set global options, use `GlobOptions.Default`.

For example:

```csharp
    // Overide the default options globally for all matche:
    GlobOptions.Default.Evaluation.CaseInsensitive = true;   
	DotNet.Globbing.Glob.Parse("foo").IsMatch("Foo"); // true; 
```

Or, override any global default options, by passing in your own instance of `GlobOptions`:

```csharp
    GlobOptions options = new GlobOptions();
    options.Evaluation.CaseInsensitive = false;
    DotNet.Globbing.Glob.Parse("foo", options).IsMatch("Foo"); // false; 

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

## Give Back

If this library has helped you, even in a small way, please consider a small donation via https://opencollective.com/darrell-tunnell
It really would be greatly appreciated.
