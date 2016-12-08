namespace DotNet.Globbing.Token
{
    public interface IVisitable<T>
    {
        void Accept(T Visitor);
    }
}
