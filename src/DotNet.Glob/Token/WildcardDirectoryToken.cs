namespace DotNet.Globbing.Token
{
    public class WildcardDirectoryToken : IGlobToken
    {
        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}