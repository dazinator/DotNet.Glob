using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public class PathSeperatorTokenEvaluator : IGlobTokenEvaluator
    {
        private readonly PathSeperatorToken _token;
        public PathSeperatorTokenEvaluator(PathSeperatorToken token)
        {
            _token = token;
            //ContinueMatch = false;
        }
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            var currentChar = allChars[currentPosition];
            newPosition = currentPosition + 1;
            return GlobStringReader.IsPathSeperator(currentChar);
        }

    }
}