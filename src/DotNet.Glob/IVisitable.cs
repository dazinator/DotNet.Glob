namespace DotNet.Globbing
{
    public interface IVisitable<T>
    {
        void Accept(T Visitor);
    }
}
