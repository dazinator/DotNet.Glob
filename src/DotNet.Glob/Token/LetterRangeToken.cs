namespace DotNet.Globbing.Token
{
    public class LetterRangeToken : RangeToken<char>
    {
        public LetterRangeToken(char start, char end, bool isNegated) : base(start, end, isNegated)
        {
        }

        public override void Accept(IGlobTokenVisitor Visitor)
        {
           Visitor.Visit(this);
        }
    }
}