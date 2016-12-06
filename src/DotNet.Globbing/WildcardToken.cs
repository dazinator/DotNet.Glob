namespace DotNet.Globbing
{
    public class WildcardToken : IGlobToken
    {
        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}