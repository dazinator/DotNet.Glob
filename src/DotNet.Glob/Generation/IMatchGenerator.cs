using System.Text;

namespace DotNet.Globbing.Generation
{
    public interface IMatchGenerator
    {
        void Append(StringBuilder builder);
    }
}