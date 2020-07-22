using System;

namespace DotNet.Globbing.Evaluation
{
    public interface IGlobTokenEvaluator
    {

#if SPAN
        bool IsMatch(ReadOnlySpan<char> allChars, int currentPosition, out int newPosition);
#endif
        bool IsMatch(string allChars, int currentPosition, out int newPosition);

        int ConsumesMinLength { get; }
        bool ConsumesVariableLength { get; }
    }
}