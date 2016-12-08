namespace DotNet.Globbing.Token
{
    public abstract class RangeToken<T> : INegatableToken
    {
        public bool IsNegated { get; set; }
        public T Start { get; set; }
        public T End { get; set; }

        public abstract void Accept(IGlobTokenVisitor Visitor);

    }
}