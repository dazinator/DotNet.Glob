namespace DotNet.Globbing.Token
{
    public class WildcardDirectoryToken : IGlobToken
    {

        public WildcardDirectoryToken(char? trailingPathSeperator)
        {
            TrailingPathSeperator = trailingPathSeperator;
        }

        //public PathSeperatorKind? TrailingPathSeperatorKind { get; set; }
        public char? TrailingPathSeperator { get; set; }

        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}