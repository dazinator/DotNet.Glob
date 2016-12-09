using DotNet.Globbing.Token;

namespace DotNet.Globbing
{
    public class GlobTokenMatch
    {
        public IGlobToken Token { get; set; }
        public string Value { get; set; }
    }
}