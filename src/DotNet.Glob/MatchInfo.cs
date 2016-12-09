using System.Runtime.CompilerServices;
using DotNet.Globbing.Token;

namespace DotNet.Globbing
{
    public class MatchInfo
    {
        public GlobTokenMatch[] Matches { get; set; }

        public IGlobToken Missed { get; set; }

        public bool Success { get; set; }

        public string UnmatchedText { get; set; }

    }
}