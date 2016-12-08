namespace DotNet.Globbing.Token
{
    public class NumberRangeToken : RangeToken<char>
    {
        public override void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }

    }
}