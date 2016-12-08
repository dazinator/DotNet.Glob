namespace DotNet.Globbing.Token
{
    public class SingleCharacterToken : IGlobToken
    {
        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}