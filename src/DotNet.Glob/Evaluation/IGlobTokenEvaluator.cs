namespace DotNet.Globbing.Evaluation
{
    public interface IGlobTokenEvaluator
    {
        bool IsMatch(string allChars, int currentPosition, out int newPosition);


    }
}