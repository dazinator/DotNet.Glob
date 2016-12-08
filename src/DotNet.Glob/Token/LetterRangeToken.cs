namespace DotNet.Globbing.Token
{
    public class LetterRangeToken : RangeToken<char>
    {
        public override void Accept(IGlobTokenVisitor Visitor)
        {
           Visitor.Visit(this);
        }
    }
}