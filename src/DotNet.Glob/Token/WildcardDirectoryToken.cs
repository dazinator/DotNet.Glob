namespace DotNet.Globbing.Token
{
    public class WildcardDirectoryToken : IGlobToken
    {

        public WildcardDirectoryToken(PathSeperatorToken leadingPathSeperator, PathSeperatorToken trailingPathSeperator)
        {
            TrailingPathSeperator = trailingPathSeperator;
            LeadingPathSeperator = leadingPathSeperator;
        }

        public PathSeperatorToken TrailingPathSeperator { get; set; }

        public PathSeperatorToken LeadingPathSeperator { get; set; }

        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}