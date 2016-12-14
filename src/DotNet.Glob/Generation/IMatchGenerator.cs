using System.Text;

namespace DotNet.Globbing.Generation
{
    public interface IMatchGenerator
    {
        void AppendMatch(StringBuilder builder);

        void AppendNonMatch(StringBuilder builder);
    }
}