using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing
{

    public interface ITokenMatcher
    {
        bool IsMatch(string allChars, int currentPosition, out int newPosition);


    }

    //public class TokenMatcher : ITokenMatcher
    //{
    //    public virtual bool IsMatch(char character)
    //    {

    //    }
    //}

    public class PathSeperatorMatcher : ITokenMatcher
    {
        private readonly PathSeperatorToken _token;
        public PathSeperatorMatcher(PathSeperatorToken token)
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

    public class LiteralTokenMatcher : ITokenMatcher
    {
        private readonly LiteralToken _token;

        public LiteralTokenMatcher(LiteralToken token)
        {
            _token = token;
        }
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            newPosition = currentPosition;
            int counter = 0;
            while (newPosition < allChars.Length && counter < _token.Value.Length)
            {
                var compareChar = _token.Value[counter];
                var currentChar = allChars[newPosition];

                if (compareChar != currentChar)
                {
                    return false;
                }

                newPosition = newPosition + 1;
                counter = counter + 1;
            }

            //foreach (var literalChar in _token.Value)
            //{
            //    var read = allChars[newPosition];
            //    //if (read == Char.MinValue)
            //    //{
            //    //    return false;
            //    //}
            //    if (read != literalChar)
            //    {
            //        return false;
            //    }
            //    if(currentPosition == )
            //    newPosition = newPosition + 1;

            //}
            return true;
        }
    }

    public class AnyCharacterTokenMatcher : ITokenMatcher
    {
        private readonly AnyCharacterToken _token;

        public AnyCharacterTokenMatcher(AnyCharacterToken token)
        {
            _token = token;
        }
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            newPosition = currentPosition + 1;
            var currentChar = allChars[currentPosition];
            //if (read == Char.MinValue)
            //{
            //    return;
            //}
            if (GlobStringReader.IsPathSeperator(currentChar))
            {
                return false;
            }

            return true;


        }
    }

    public class LetterRangeTokenMatcher : ITokenMatcher
    {
        private readonly LetterRangeToken _token;

        public LetterRangeTokenMatcher(LetterRangeToken token)
        {
            _token = token;
        }
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            var currentChar = allChars[currentPosition];
            newPosition = currentPosition + 1;

            //var currentChar = (char)read;
            if (currentChar >= _token.Start && currentChar <= _token.End)
            {
                if (_token.IsNegated)
                {
                    return false; // failed to match
                }
            }
            else
            {
                if (!_token.IsNegated)
                {
                    return false; // failed to match
                }
            }

            return true;
            // this.Success = true;


        }
    }

    public class NumberRangeTokenMatcher : ITokenMatcher
    {
        private readonly NumberRangeToken _token;

        public NumberRangeTokenMatcher(NumberRangeToken token)
        {
            _token = token;
        }
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            var currentChar = allChars[currentPosition];
            newPosition = currentPosition + 1;

            //var currentChar = (char)read;
            if (currentChar >= _token.Start && currentChar <= _token.End)
            {
                if (_token.IsNegated)
                {
                    return false; // failed to match
                }
            }
            else
            {
                if (!_token.IsNegated)
                {
                    return false; // failed to match
                }
            }

            return true;
            // this.Success = true;


        }
    }

    public class CharacterListTokenMatcher : ITokenMatcher
    {
        private readonly CharacterListToken _token;

        public CharacterListTokenMatcher(CharacterListToken token)
        {
            _token = token;
        }
        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            var currentChar = allChars[currentPosition];
            newPosition = currentPosition + 1;

            //var currentChar = (char)read;
            var contains = _token.Characters.Contains(currentChar);
            if (_token.IsNegated)
            {
                if (contains)
                {
                    return false;
                }
            }
            else
            {
                if (!contains)
                {
                    return false;
                }
            }

            return true;
        }
    }

    //public class WildcardTokenMatcher : ITokenMatcher
    //{
    //    private readonly WildcardToken _token;

    //    private readonly IGlobToken[] _childTokens;

    //    public WildcardTokenMatcher(WildcardToken token, IGlobToken[] childTokens)
    //    {
    //        _token = token;
    //        _childTokens = childTokens;


    //    }

    //    public bool IsMatch(char[] allChars, int currentPosition, out int newPosition)
    //    {
    //        var currentChar = allChars[currentPosition];
    //        newPosition = currentPosition + 1;


    //    }


    //    public char ReadChar()
    //    {
    //        if (_currentCharIndex >= _text.Length)
    //        {
    //            return Char.MinValue;
    //        }
    //        var result = _text[_currentCharIndex];
    //        _currentCharIndex = _currentCharIndex + 1;

    //        return result;
    //    }

    //    public string ReadRemaining(out int positionOfNextPathSeperator)
    //    {
    //        var builder = new StringBuilder(_text.Length - _currentCharIndex);
    //        var nextChar = ReadChar();
    //        positionOfNextPathSeperator = 0;
    //        while (nextChar != Char.MinValue)
    //        {
    //            builder.Append(nextChar);
    //            if (nextChar == '/' || nextChar == '\\')
    //            {
    //                break;
    //            }
    //            else
    //            {
    //                positionOfNextPathSeperator = positionOfNextPathSeperator + 1;
    //            }
    //            nextChar = ReadChar();
    //        }           

    //        return builder.ToString();

    //    }

    //}

    public class GlobIsMatchTokenEvaluator : IGlobTokenVisitor, ITokenMatcher
    {
        private IGlobToken[] _Tokens;
        private List<ITokenMatcher> _Matchers;

        //  public string _text;
        private bool _finished = false;
        public int _currentCharIndex;
        public int _currentTokenIndex;
        public int _currentChar;
        // private bool _isWildCard;

        //public int CurrentChar { get { return _text[_currentCharIndex]; } }

        public GlobIsMatchTokenEvaluator(IGlobToken[] tokens)
        {
            // _Reader = reader;
            _Tokens = tokens;
            _Matchers = new List<ITokenMatcher>();
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

        //private void EnqueueTokens(IGlobToken[] tokens)
        //{
        //    _TokenQueue.Clear();
        //    foreach (var token in tokens)
        //    {
        //        _TokenQueue.Enqueue(token);
        //    }
        //}

        public bool IsMatch(string text)
        {
            //  EnqueueTokens(_Tokens);
            // _text = text;
            _currentCharIndex = 0;

            foreach (var matcher in _Matchers)
            {
                if (!matcher.IsMatch(text, _currentCharIndex, out _currentCharIndex))
                {
                    return false;
                }
            }
            //_currentTokenIndex = 0;
            // IGlobToken token = null;

            //using (_Reader = new GlobStringReader(text))
            //{

            //for (int _currentCharIndex = 0; _currentCharIndex < text.Length; _currentCharIndex++)
            //{
            //    _currentChar = text[_currentCharIndex];

            //}


            //while (_TokenQueue.Any())
            //{
            //    Success = false;
            //    token = _TokenQueue.Dequeue();
            //    token.Accept(this);
            //    if (!Success)
            //    {
            //        return false;
            //    }
            //    if (_finished)
            //    {
            //        break;
            //    }
            //}

            // if all tokens matched but still more text then fail!
            if (_currentCharIndex < text.Length)
            {
                return false;
            }
            //return false;
            //    if (_Reader.Peek() != -1)
            //    {
            //        return false;
            //    }

            // Success.
            return true;
            // }
        }


        public void Visit(PathSeperatorToken token)
        {
            _Matchers.Add(new PathSeperatorMatcher(token));

            //var read = ReadChar();
            //if (read == Char.MinValue)
            //{
            //    return;
            //}
            //if (!GlobStringReader.IsPathSeperator(read))
            //{
            //    return;
            //}
            //this.Success = true;
        }

        public void Visit(LiteralToken token)
        {
            _Matchers.Add(new LiteralTokenMatcher(token));
            //foreach (var literalChar in token.Value)
            //{
            //    var read = ReadChar();
            //    if (read == Char.MinValue)
            //    {
            //        return;
            //    }
            //    if (read != literalChar)
            //    {
            //        return;
            //    }
            //}
            //this.Success = true;
        }

        public void Visit(AnyCharacterToken token)
        {
            _Matchers.Add(new AnyCharacterTokenMatcher(token));
        }

        public void Visit(LetterRangeToken token)
        {
            _Matchers.Add(new LetterRangeTokenMatcher(token));
        }

        public void Visit(NumberRangeToken token)
        {
            _Matchers.Add(new NumberRangeTokenMatcher(token));
        }

        public void Visit(CharacterListToken token)
        {
            _Matchers.Add(new CharacterListTokenMatcher(token));
        }

        public void Visit(WildcardToken token)
        {
            int remainingCount = _Tokens.Length - (_currentTokenIndex + 1);
            // if no more tokens then just return as * matches the rest of the segment, and therefore no more matching.
            if (remainingCount == 0)
            {
                return;
            }

            IGlobToken[] remaining = new IGlobToken[remainingCount];
            Array.Copy(_Tokens, _currentTokenIndex + 1, remaining, 0, remainingCount);
            _Matchers.Add(new GlobIsMatchTokenEvaluator(remaining));
            _finished = true; // stop visiting any further tokens as we have offloaded them to a child evaluator.
        }

        public bool IsMatch(string allChars, int currentPosition, out int newPosition)
        {
            newPosition = currentPosition;
            // When * encountered,
            // Dequees all remaining tokens and passes them to a nested Evaluator.
            // Keeps seing if the nested evaluator will match, and if it doesn't then
            // will consume / match against one character, and retry.
            // Exits when match successful, or when the end of the current path segment is reached.
            int endOfSegmentPos;
            var remainingText = ReadRemaining(allChars, out endOfSegmentPos);

            // we have to attempt to match the remaining tokens, and if they dont all match,
            // then consume a character, until we have matched the entirity of this segment.
            var matchedText = new StringBuilder(endOfSegmentPos);
            // var nestedEval = new GlobIsMatchTokenEvaluator(tokens);
            // var pathSegments = new List<string>();

            for (int i = 0; i < endOfSegmentPos; i++)
            {
                var isMatch = IsMatch(remainingText);
                if (isMatch)
                {
                    return true;
                }

                // match a single character
                matchedText.Append(remainingText[0]);
                // re-attempt matching of child tokens against this remaining string.
                remainingText = remainingText.Substring(1);

            }

            return false;
        }

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

    public class GlobTokenEvaluator : IGlobTokenVisitor
    {
        private IGlobToken[] _Tokens;
        private GlobStringReader _Reader;
        public Queue<IGlobToken> _TokenQueue;
        public string _text;

        public List<GlobTokenMatch> MatchedTokens { get; set; }

        public GlobTokenEvaluator(IGlobToken[] tokens)
        {
            // _Reader = reader;
            _Tokens = tokens;
            var tokenCount = tokens.Length;
            _TokenQueue = new Queue<IGlobToken>(tokenCount);
            MatchedTokens = new List<GlobTokenMatch>(tokens.Length);
        }

        private void EnqueueTokens(IGlobToken[] tokens)
        {
            MatchedTokens.Clear();
            _TokenQueue.Clear();
            foreach (var token in tokens)
            {
                _TokenQueue.Enqueue(token);
            }
        }

        public MatchInfo Evaluate(string text)
        {
            EnqueueTokens(_Tokens);
            _text = text;
            IGlobToken token = null;
            using (_Reader = new GlobStringReader(text))
            {
                while (_TokenQueue.Any())
                {
                    token = _TokenQueue.Dequeue();
                    token.Accept(this);
                    if (!Success)
                    {
                        return FailedResult(token);
                    }
                }

                // if all tokens matched but still more text then fail!
                if (_Reader.Peek() != -1)
                {
                    return FailedResult(null);
                }

                // Success.
                return SuccessfulResult();
            }
        }

        private MatchInfo SuccessfulResult()
        {
            return new MatchInfo()
            {
                Matches = MatchedTokens.ToArray(),
                Missed = null,
                Success = true,
                UnmatchedText = _Reader.ReadToEnd()
            };
        }

        private MatchInfo FailedResult(IGlobToken token)
        {
            return new MatchInfo()
            {
                Matches = MatchedTokens.ToArray(),
                Missed = token,
                Success = false,
                UnmatchedText = _Reader.ReadToEnd()
            };
        }

        public void Visit(PathSeperatorToken token)
        {
            Success = false;
            var read = _Reader.Read();
            if (read == -1)
            {
                return;
            }

            var currentChar = (char)read;
            if (!GlobStringReader.IsPathSeperator(currentChar))
            {
                return;
            }

            AddMatch(new GlobTokenMatch() { Token = token, Value = currentChar.ToString() });
        }

        public void Visit(LiteralToken token)
        {
            Success = false;
            foreach (var literalChar in token.Value)
            {
                var read = _Reader.Read();
                if (read == -1)
                {
                    return;
                }
                var currentChar = (char)read;
                if (currentChar != literalChar)
                {
                    return;
                }
            }

            AddMatch(new GlobTokenMatch() { Token = token });

        }

        public void Visit(AnyCharacterToken token)
        {
            //WithWildcardProgression(() =>
            //{
            Success = false;
            var read = _Reader.Read();
            if (read == -1)
            {
                return;
            }
            var currentChar = (char)read;
            if (GlobStringReader.IsPathSeperator(currentChar))
            {
                return;
            }

            AddMatch(new GlobTokenMatch() { Token = token, Value = currentChar.ToString() });
        }

        public void Visit(WildcardToken token)
        {
            // When * encountered,
            // Dequees all remaining tokens and passes them to a nested Evaluator.
            // Keeps seing if the nested evaluator will match, and if it doesn't then
            // will consume / match against one character, and retry.
            // Exits when match successful, or when the end of the current path segment is reached.
            GlobTokenMatch match = null;

            match = new GlobTokenMatch() { Token = token };
            AddMatch(match);


            var remainingText = _Reader.ReadToEnd();
            int endOfSegmentPos;
            using (var pathReader = new GlobStringReader(remainingText))
            {
                var thisPath = pathReader.ReadPathSegment();
                endOfSegmentPos = pathReader.CurrentIndex;
            }

            var remaining = _TokenQueue.ToArray();
            // if no more tokens remaining then just return as * matches the rest of the segment.
            if (remaining.Length == 0)
            {
                this.Success = true;

                match.Value = remainingText;


                return;
            }

            // we have to attempt to match the remaining tokens, and if they dont all match,
            // then consume a character, until we have matched the entirity of this segment.
            var matchedText = new StringBuilder(endOfSegmentPos);
            var nestedEval = new GlobTokenEvaluator(remaining);
            var pathSegments = new List<string>();

            // we keep a record of text that this wildcard matched in order to satisfy the
            // greatest number of child token matches.
            var bestMatchText = new StringBuilder(endOfSegmentPos);
            IList<GlobTokenMatch> bestMatches = null; // the most tokens that were matched.

            for (int i = 0; i < endOfSegmentPos; i++)
            {
                var matchInfo = nestedEval.Evaluate(remainingText);
                if (matchInfo.Success)
                {
                    break;
                }

                // match a single character
                matchedText.Append(remainingText[0]);
                // re-attempt matching of child tokens against this remaining string.
                remainingText = remainingText.Substring(1);
                // If we have come closer to matching, record our best results.
                if ((bestMatches == null && matchInfo.Matches.Any()) || (bestMatches != null && bestMatches.Count < matchInfo.Matches.Length))
                {
                    bestMatches = matchInfo.Matches.ToArray();
                    bestMatchText.Clear();
                    bestMatchText.Append(matchedText.ToString());
                }

            }

            this.Success = nestedEval.Success;
            if (nestedEval.Success)
            {
                // add all child matches.
                this.MatchedTokens.AddRange(nestedEval.MatchedTokens);

            }
            else
            {
                // add the most tokens we managed to match.
                if (bestMatches != null && bestMatches.Any())
                {
                    this.MatchedTokens.AddRange(bestMatches);
                }
            }

            match.Value = matchedText.ToString();
            _TokenQueue.Clear();
        }

        public void Visit(LetterRangeToken token)
        {
            Success = false;
            var read = _Reader.Read();
            if (read == -1)
            {
                return;
            }
            var currentChar = (char)read;
            if (currentChar >= token.Start && currentChar <= token.End)
            {
                if (token.IsNegated)
                {
                    return; // failed to match
                }
            }
            else
            {
                if (!token.IsNegated)
                {
                    return; // failed to match
                }
            }

            AddMatch(new GlobTokenMatch() { Token = token, Value = currentChar.ToString() });



        }

        public void Visit(NumberRangeToken token)
        {
            Success = false;
            var read = _Reader.Read();
            if (read == -1)
            {
                return;
            }
            var currentChar = (char)read;
            if (currentChar >= token.Start && currentChar <= token.End)
            {
                if (token.IsNegated)
                {
                    return; // failed to match
                }
            }
            else
            {
                if (!token.IsNegated)
                {
                    return; // failed to match
                }
            }


            AddMatch(new GlobTokenMatch() { Token = token, Value = currentChar.ToString() });


        }

        public void Visit(CharacterListToken token)
        {
            Success = false;
            var read = _Reader.Read();
            if (read == -1)
            {
                return;
            }
            var currentChar = (char)read;
            var contains = token.Characters.Contains(currentChar);

            if (token.IsNegated)
            {
                if (contains)
                {
                    return;
                }
            }
            else
            {
                if (!contains)
                {
                    return;
                }
            }


            AddMatch(new GlobTokenMatch() { Token = token, Value = currentChar.ToString() });


        }

        public bool Success { get; set; }

        private void AddMatch(GlobTokenMatch match)
        {
            this.MatchedTokens.Add(match);
            this.Success = true;
        }
    }
}