namespace DotNet.Globbing.Token
{
    public interface INegatableToken : IGlobToken
    {
        bool IsNegated { get; set; }
    }
}