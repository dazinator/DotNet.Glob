using System;
using System.Collections.Generic;
using DotNet.Globbing.Token;
using System.Linq;

namespace DotNet.Globbing.Evaluation
{
    public class CompositeTokenEvaluator : IGlobTokenEvaluator, IGlobTokenVisitor
    {
        private IGlobToken[] _Tokens;
        private List<IGlobTokenEvaluator> _Evaluators;
        public int _currentTokenIndex;
        private bool _finished = false;

        public CompositeTokenEvaluator(IGlobToken[] tokens)
        {
            ConsumesVariableLength = tokens.Length == 0; // the only time we pass in 0 tokens is for a wildcard at the end of glob pattern.
            ConsumesMinLength = 0;
            _Tokens = tokens;
            _Evaluators = new List<IGlobTokenEvaluator>();
            _currentTokenIndex = 0;
            foreach (var token in _Tokens)
            {
                token.Accept(this);
                if (_finished)
                { // stop visiting any more. Usually happens if encountering wildcard.
                    break;
                }
                _currentTokenIndex = _currentTokenIndex + 1;
            }

        }

        public void Visit(PathSeperatorToken token)
        {
            AddEvaluator(new PathSeperatorTokenEvaluator(token));
        }

        public void Visit(LiteralToken token)
        {
            AddEvaluator(new LiteralTokenEvaluator(token));
        }

        public void Visit(AnyCharacterToken token)
        {
            AddEvaluator(new AnyCharacterTokenEvaluator(token));
        }

        public void Visit(LetterRangeToken token)
        {
            AddEvaluator(new LetterRangeTokenEvaluator(token));
        }

        public void Visit(NumberRangeToken token)
        {
            AddEvaluator(new NumberRangeTokenEvaluator(token));
        }

        public void Visit(CharacterListToken token)
        {
            AddEvaluator(new CharacterListTokenEvaluator(token));
        }
        public void Visit(WildcardToken token)
        {
            // if no more tokens then just return as * matches the rest of the segment, and therefore no more matching.
            int remainingCount = _Tokens.Length - (_currentTokenIndex + 1);
            //if (remainingCount == 0)
            //{
            //    var subEvaluator = new WildcardDirectoryTokenEvaluator(token, new CompositeTokenEvaluator(new IGlobToken[]));
            //}

            // Add a nested CompositeTokenEvaluator, passing all of our remaining tokens to it.
            IGlobToken[] remaining = new IGlobToken[remainingCount];
            Array.Copy(_Tokens, _currentTokenIndex + 1, remaining, 0, remainingCount);
            AddEvaluator(new WildcardTokenEvaluator(token, new CompositeTokenEvaluator(remaining)));

            //  _Evaluators.Add(new CompositeEvaluator(remaining));
            _finished = true; // signlas to stop visiting any further tokens as we have offloaded them all to the nested evaluator.
        }

        public void Visit(WildcardDirectoryToken token)
        {
            // if no more tokens then just return as * matches the rest of the segment, and therefore no more matching.
            int remainingCount = _Tokens.Length - (_currentTokenIndex + 1);
            //if (remainingCount == 0)
            //{
            //    return;
            //}

            // Add a nested CompositeTokenEvaluator, passing all of our remaining tokens to it.
            IGlobToken[] remaining = new IGlobToken[remainingCount];
            Array.Copy(_Tokens, _currentTokenIndex + 1, remaining, 0, remainingCount);
            var subEvaluator = new WildcardDirectoryTokenEvaluator(token, new CompositeTokenEvaluator(remaining));
            AddEvaluator(subEvaluator);

            //  _Evaluators.Add(new CompositeEvaluator(remaining));
            _finished = true; // signlas to stop visiting any further tokens as we have offloaded them all to the nested evaluator.
        }

        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            newPosition = currentPosition;

            if (!ConsumesVariableLength)
            {
                if ((allChars.Length - currentPosition) != this.ConsumesMinLength)
                {
                    // can't possibly match as tokens require a fixed length and the string length is different.
                    return false;
                }
            }
            else if ((allChars.Length - currentPosition) < this.ConsumesMinLength)
            {
                // can't possibly match as tokens require a minimum length and the string is too short.
                return false;
            }

            //if (_Evaluators.Count == 0)
            //{
            //    // no sub evaluators in this composite.
            //    // happens for wildcards that are at the end of a pattern.
            //    return true;
            //}

            foreach (var matcher in _Evaluators)
            {
                if (!matcher.IsMatch(allChars, newPosition, out newPosition))
                {
                    return false;
                }
            }
            // if all tokens matched but still more text then fail!
            if (newPosition < allChars.Length - 1)
            {
                return false;
            }
            // Success.
            return true;
        }

        public int ConsumesMinLength { get; protected set; }

        public bool ConsumesVariableLength { get; protected set; }

        public int EvaluatorCount { get { return _Evaluators.Count; } }

        protected void AddEvaluator(IGlobTokenEvaluator evaluator)
        {
            _Evaluators.Add(evaluator);
            this.ConsumesMinLength = this.ConsumesMinLength + evaluator.ConsumesMinLength;
            if (!ConsumesVariableLength)
            {
                if (evaluator.ConsumesVariableLength)
                {
                    this.ConsumesVariableLength = true;
                }
            }
        }

    }
}