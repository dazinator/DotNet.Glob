namespace DotNet.Globbing.Token
{
    public class WildcardDirectoryToken : IGlobToken
    {

        public WildcardDirectoryToken(PathSeperatorToken trailingPathSeperator, PathSeperatorToken leadingPathSeperator)
        {
            TrailingPathSeperator = trailingPathSeperator;
            LeadingPathSeperator = leadingPathSeperator;
        }

        //public PathSeperatorKind? TrailingPathSeperatorKind { get; set; }
        public PathSeperatorToken TrailingPathSeperator { get; set; }

        public PathSeperatorToken LeadingPathSeperator { get; set; }

        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}