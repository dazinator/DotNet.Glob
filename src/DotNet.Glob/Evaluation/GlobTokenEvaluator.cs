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
            //if (!matched)
            //{
            //    return false;
            //}
            //foreach (var matcher in _Evaluators)
            //{
            //    if (!matcher.IsMatch(allChars, newPosition, out newPosition))
            //    {
            //        return false;
            //    }
            //}
            //// if all tokens matched but still more text then fail!
            //if (newPosition < allChars.Length)
            //{
            //    return false;
            //}
            // Success.
            // return true;
        }

    }
}