namespace DotNet.Globbing
{
    public class PathSeperatorToken : IGlobToken
    {
        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}