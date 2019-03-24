namespace DotNet.Globbing.Token
{
    public class WildcardDirectoryToken : IGlobToken
    {

        public WildcardDirectoryToken(PathSeparatorToken leadingPathSeparator, PathSeparatorToken trailingPathSeparator)
        {
            TrailingPathSeparator = trailingPathSeparator;
            LeadingPathSeparator = leadingPathSeparator;
        }

        public PathSeparatorToken TrailingPathSeparator { get; set; }

        public PathSeparatorToken LeadingPathSeparator { get; set; }

        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}