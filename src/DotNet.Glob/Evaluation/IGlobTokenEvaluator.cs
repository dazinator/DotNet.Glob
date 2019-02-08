using System;

namespace DotNet.Globbing.Evaluation
{
    public interface IGlobTokenEvaluator
    {
        bool IsMatch(string allChars, int currentPosition, out int newPosition);
#if NETCOREAPP2_1
        bool IsMatch(ReadOnlySpan<char> allChars, int currentPosition, out int newPosition);
#endif
        int ConsumesMinLength { get; }
        bool ConsumesVariableLength { get; }
    }
}