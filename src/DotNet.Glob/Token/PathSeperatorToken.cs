namespace DotNet.Globbing.Token
{
    public class PathSeperatorToken : IGlobToken
    {
        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}