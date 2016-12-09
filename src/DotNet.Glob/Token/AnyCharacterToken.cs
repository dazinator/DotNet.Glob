namespace DotNet.Globbing.Token
{
    public class AnyCharacterToken : IGlobToken
    {
        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}