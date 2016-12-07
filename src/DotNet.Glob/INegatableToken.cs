namespace DotNet.Globbing
{
    public interface INegatableToken : IGlobToken
    {
        bool IsNegated { get; set; }
    }
}