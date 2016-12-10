using System;
using System.Collections.Generic;
using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public class GlobTokenEvaluator : IGlobTokenVisitor
    {
        private IGlobToken[] _Tokens;
        private List<IGlobTokenEvaluator> _Evaluators;
        private bool _finished = false;
        public int _currentCharIndex;
        public int _currentTokenIndex;
        public int _currentChar;

        /// <summary>
        /// This represents the minimum length of the string that can match the pattern.
        /// Enables us to fail early by simple string length checking.
        /// </summary>
        public int MinimumRequiredLength { get; set; }


        public GlobTokenEvaluator(IGlobToken[] tokens)
        {
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

        public bool IsMatch(string text)
        {
            _currentCharIndex = 0;
            foreach (var matcher in _Evaluators)
            {
                if (!matcher.IsMatch(text, _currentCharIndex, out _currentCharIndex))
                {
                    return false;
                }
            }
            // if all tokens matched but still more text then fail!
            if (_currentCharIndex < text.Length)
            {
                return false;
            }
            // Success.
            return true;
        }

        public void Visit(PathSeperatorToken token)
        {
            _Evaluators.Add(new PathSeperatorTokenEvaluator(token));
        }

        public void Visit(LiteralToken token)
        {
            _Evaluators.Add(new LiteralTokenEvaluator(token));
        }

        public void Visit(AnyCharacterToken token)
        {
            _Evaluators.Add(new AnyCharacterTokenEvaluator(token));
        }

        public void Visit(LetterRangeToken token)
        {
            _Evaluators.Add(new LetterRangeTokenEvaluator(token));
        }

        public void Visit(NumberRangeToken token)
        {
            _Evaluators.Add(new NumberRangeTokenEvaluator(token));
        }

        public void Visit(CharacterListToken token)
        {
            _Evaluators.Add(new CharacterListTokenEvaluator(token));
        }

        public void Visit(WildcardToken token)
        {
            // if no more tokens then just return as * matches the rest of the segment, and therefore no more matching.
            int remainingCount = _Tokens.Length - (_currentTokenIndex + 1);
            if (remainingCount == 0)
            {
                return;
            }

            // Add a nested CompositeTokenEvaluator, passing all of our remaining tokens to it.
            IGlobToken[] remaining = new IGlobToken[remainingCount];
            Array.Copy(_Tokens, _currentTokenIndex + 1, remaining, 0, remainingCount);
            _Evaluators.Add(new WildcardTokenEvaluator(token, new GlobTokenEvaluator(remaining)));

            //  _Evaluators.Add(new CompositeEvaluator(remaining));
            _finished = true; // signlas to stop visiting any further tokens as we have offloaded them all to the nested evaluator.
        }

        public void Visit(WildcardDirectoryToken token)
        {
            // if no more tokens then just return as * matches the rest of the segment, and therefore no more matching.
            int remainingCount = _Tokens.Length - (_currentTokenIndex + 1);
            if (remainingCount == 0)
            {
                return;
            }

            // Add a nested CompositeTokenEvaluator, passing all of our remaining tokens to it.
            IGlobToken[] remaining = new IGlobToken[remainingCount];
            Array.Copy(_Tokens, _currentTokenIndex + 1, remaining, 0, remainingCount);
            _Evaluators.Add(new WildcardDirectoryTokenEvaluator(token, new GlobTokenEvaluator(remaining)));

            //  _Evaluators.Add(new CompositeEvaluator(remaining));
            _finished = true; // signlas to stop visiting any further tokens as we have offloaded them all to the nested evaluator.
        }

    }
}