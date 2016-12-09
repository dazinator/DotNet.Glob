namespace DotNet.Globbing.Token
{
    public abstract class RangeToken<T> : INegatableToken
    {

        protected RangeToken(T start, T end, bool isNegated)
        {
            Start = start;
            End = end;
            IsNegated = isNegated;
        }

        public bool IsNegated { get; set; }
        public T Start { get; set; }
        public T End { get; set; }

        public abstract void Accept(IGlobTokenVisitor Visitor);

    }
}