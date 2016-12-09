namespace DotNet.Globbing.Token
{
    public class NumberRangeToken : RangeToken<char>
    {
        public NumberRangeToken(char start, char end, bool isNegated) : base(start, end, isNegated)
        {
        }

        public override void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }

    }
}