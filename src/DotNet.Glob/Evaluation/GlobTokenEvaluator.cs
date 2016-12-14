using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public class GlobTokenEvaluator
    {

        public int _currentCharIndex;
        public int _currentChar;

        private readonly CompositeTokenEvaluator _rootTokenEvaluator;

        public GlobTokenEvaluator(IGlobToken[] tokens)
        {
            _rootTokenEvaluator = new CompositeTokenEvaluator(tokens);
        }

        public bool IsMatch(string text)
        {
            int finalPosition = 0;
            var matched = _rootTokenEvaluator.IsMatch(text, 0, out finalPosition);
            return matched;
        }

    }
}