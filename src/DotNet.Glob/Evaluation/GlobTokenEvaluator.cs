using DotNet.Globbing.Token;
using System;

namespace DotNet.Globbing.Evaluation
{
    public class GlobTokenEvaluator
    {

        public int _currentCharIndex;
        public int _currentChar;

        private readonly CompositeTokenEvaluator _rootTokenEvaluator;
        private readonly EvaluationOptions _options;

        public GlobTokenEvaluator(EvaluationOptions options, IGlobToken[] tokens)
        {
            _options = options;
            _rootTokenEvaluator = new CompositeTokenEvaluator(tokens, options.GetTokenEvaluatorFactory());
        }

        public bool IsMatch(string subject)
        {
            int finalPosition = 0;
#if SPAN
            var matched = _rootTokenEvaluator.IsMatch(subject.AsSpan(), 0, out finalPosition);
#else
            var matched = _rootTokenEvaluator.IsMatch(subject, 0, out finalPosition);
#endif

            return matched;
        }

#if SPAN
        public bool IsMatch(ReadOnlySpan<char> subject)
        {
            int finalPosition = 0;
            var matched = _rootTokenEvaluator.IsMatch(subject, 0, out finalPosition);
            return matched;
        }
#endif




    }

}