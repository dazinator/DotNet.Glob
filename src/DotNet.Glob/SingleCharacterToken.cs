namespace DotNet.Globbing
{
    public class SingleCharacterToken : IGlobToken
    {
        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}