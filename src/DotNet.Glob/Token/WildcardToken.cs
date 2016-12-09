namespace DotNet.Globbing.Token
{
    public class WildcardToken : IGlobToken
    {
        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}