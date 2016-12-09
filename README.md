# DotNet.Glob
A fast globbing library for .NET applications.
This library **does not** use Regex.


| Branch  | Build Status | NuGet |
| ------------- | ------------- | ----- |
| Master  |[![Build master](https://ci.appveyor.com/api/projects/status/yab1btvh7bvkkgva/branch/master?svg=true)](https://ci.appveyor.com/project/dazinator/dotnet-glob/branch/master) | [![NuGet](https://img.shields.io/nuget/v/DotNet-Glob.svg)](https://www.nuget.org/packages/DotNet.Glob/) |
| Develop | [![Build develop](https://ci.appveyor.com/api/projects/status/yab1btvh7bvkkgva?svg=true)](https://ci.appveyor.com/project/dazinator/dotnet-glob/branch/develop)  | [![NuGet](https://img.shields.io/nuget/vpre/DotNet-Glob.svg)](https://www.nuget.org/packages/DotNet-Glob/) |

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



# Advanced Usages

## Match Generation
Given a glob, you can generate random matches for that glog. This can be useful when testing etc.

```
  var dotnetGlob = Glob.Parse(pattern);
  var generator = new GlobMatchStringGenerator(dotnetGlob.Tokens);

  for (int i = 0; i < 10; i++)
      {
          var testString = generator.GenerateRandomMatch();
          var result = dotnetGlob.IsMatch(testString);
          // result is always true.
      }

```

## Match Analysis

The `IsMatch` method just returns you a boolean. If you require more in-depth information about the match including which tokens were matched, or failed to match, you can do this:

```
 MatchInfo match = glob.Match(somestring);

```

You can then inspect the `MatchInfo` which holds all of those useful details.

