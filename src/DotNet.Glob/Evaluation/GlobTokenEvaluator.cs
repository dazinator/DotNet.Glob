using System;
using System.Collections.Generic;
using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public class CompositeEvaluator : IGlobTokenVisitor, IGlobTokenEvaluator
    {
        private IGlobToken[] _Tokens;
        private List<IGlobTokenEvaluator> _Evaluators;
        private bool _finished = false;
        public int _currentCharIndex;
        public int _currentTokenIndex;
        public int _currentChar;

        public CompositeEvaluator(IGlobToken[] tokens)
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
            _Evaluators.Add(new CompositeEvaluator(remaining));
            _finished = true; // signlas to stop visiting any further tokens as we have offloaded them all to the nested evaluator.
        }


        #region IGlobTokenEvaluator

        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            // When called as an evaluator, we act as a wildcard evaluator - we only return true,
            // if all of our child evaluators return true.
            // but as we are a wildcard, we keep advancing a character in the current path segment and retrying
            // until we find a match or reach the end of the string.
            newPosition = currentPosition;
            int endOfSegmentPos;
            var remainingText = ReadRemaining(allChars, out endOfSegmentPos);

            //  var matchedText = new StringBuilder(endOfSegmentPos);

            for (int i = 0; i < endOfSegmentPos; i++)
            {
                var isMatch = IsMatch(remainingText);
                if (isMatch)
                {
                    return true;
                }

                // match a single character
                //  matchedText.Append(remainingText[0]);
                // re-attempt matching of child tokens against this remaining string.
                remainingText = remainingText.Substring(1);

            }

            return false;
        }

        #endregion

        public char ReadChar(string text)
        {
            if (_currentCharIndex >= text.Length)
            {
                return Char.MinValue;
            }
            var result = text[_currentCharIndex];
            _currentCharIndex = _currentCharIndex + 1;

            return result;
        }

        public string ReadRemaining(string text, out int positionOfNextPathSeperator)
        {
            var builder = new StringBuilder(text.Length - _currentCharIndex);
            var nextChar = ReadChar(text);
            positionOfNextPathSeperator = 0;
            while (nextChar != Char.MinValue)
            {
                builder.Append(nextChar);
                if (nextChar == '/' || nextChar == '\\')
                {
                    break;
                }
                else
                {
                    positionOfNextPathSeperator = positionOfNextPathSeperator + 1;
                }
                nextChar = ReadChar(text);
            }

            return builder.ToString();

        }

    }
}