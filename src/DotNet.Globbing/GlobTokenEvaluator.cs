using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Globbing
{
    public class GlobTokenEvaluator : IGlobTokenVisitor
    {
        private IGlobToken[] _Tokens;
        private GlobStringReader _Reader;
        // private bool IsWildcardActive = false;
        // private int TokenIndex;

        // public IGlobToken CurrentToken { get; set; }
        //public int TokenIndex { get; set; }


        public Queue<IGlobToken> _TokenQueue;

        public List<GlobTokenMatch> MatchedTokens { get; set; }

        //public GlobTokenEvaluator(IList<IGlobToken> tokens)
        //{
        //    // _Reader = reader;
        //    _Tokens = tokens;

        //    //  _TokenQueue = new Queue<IChangeToken>();
        //    MatchedTokens = new List<GlobTokenMatch>(_Tokens.Count);
        //}

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
            //  IGlobToken token = _TokenQueue.Dequeue();
            using (_Reader = new GlobStringReader(text))
            {
                while (_TokenQueue.Any())
                {
                    IGlobToken token = _TokenQueue.Dequeue();
                    // We just won't get in here...
                    token.Accept(this);
                    if (!Success)
                    {
                        return new MatchInfo()
                        {
                            Matches = MatchedTokens.ToArray(),
                            Missed = token,
                            Success = false,
                            UnmatchedText = _Reader.ReadToEnd()
                        };
                    }
                }

                // if all tokens matched but still more text then fail!
                if (_Reader.Peek() != -1)
                {
                    return new MatchInfo()
                    {
                        Matches = MatchedTokens.ToArray(),
                        Missed = null,
                        Success = false,
                        UnmatchedText = _Reader.ReadToEnd()
                    };
                }

                // Success.
                return new MatchInfo()
                {
                    Matches = MatchedTokens.ToArray(),
                    Missed = null,
                    Success = true,
                    UnmatchedText = _Reader.ReadToEnd()
                };
            }
        }

        public void Visit(PathSeperatorToken token)
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
            if (!GlobStringReader.IsPathSeperator(currentChar))
            {
                return;
            }

            AddMatch(new GlobTokenMatch() { Token = token, Value = currentChar.ToString() });
            //Success = true;
            //});


        }

        //public void WithWildcardProgression(Action action)
        //{
        //    if (IsWildcardActive)
        //    {

        //        var segment = _Reader.ReadPathSegment();
        //        _Reader = new GlobStringReader(segment);
        //        var currentReader = _Reader;
        //        try
        //        {
        //            while (true)
        //            {
        //                action();
        //                if (!Success)
        //                {
        //                    // progress wildcard and retry.
        //                    var read = currentReader.Read();
        //                    if (read == -1)
        //                    {
        //                        return;
        //                    }
        //                    var currentChar = (char)read;
        //                    if (GlobStringReader.IsPathSeperator(currentChar))
        //                    {
        //                        // wildcard only matches against current path segment.
        //                        return;
        //                    }
        //                    // swap out reader for a reader against just this segment.

        //                }
        //                else
        //                {
        //                    // wildcard matched - disable it.
        //                    IsWildcardActive = false;
        //                    return;
        //                }
        //            }
        //        }
        //        finally
        //        {
        //            _Reader = currentReader;
        //        }
        //    }
        //    else
        //    {
        //        action();
        //    }
        //}

        public void Visit(LiteralToken token)
        {
            //WithWildcardProgression(() =>
            //{
            // char[] buffer = new char[token.Value.Length];
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

        public void Visit(SingleCharacterToken token)
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
            // Success = true;
            // });
        }

        public void Visit(WildcardToken token)
        {
            // When * encountered,
            // Dequees all remaining tokens and passes them to a nested Evaluator.
            // Keeps seing if the nested evaluator will match, and if it doesn't then
            // will consume / match against one character, and retry.
            // Exits when match successful, or when the end of the current path segment is reached.
            var match = new GlobTokenMatch() { Token = token };
            AddMatch(match);

            var remaining = _TokenQueue.ToArray();
            var nestedEval = new GlobTokenEvaluator(remaining);
            var pathSegments = new List<string>();

            var remainingText = _Reader.ReadToEnd();
            int endOfSegmentPos;

            using (var pathReader = new GlobStringReader(remainingText))
            {
                var thisPath = pathReader.ReadPathSegment();
                endOfSegmentPos = pathReader.CurrentIndex;
            }

            var matchedText = new StringBuilder(endOfSegmentPos);
            var bestMatchText = new StringBuilder(endOfSegmentPos);
            IList<GlobTokenMatch> bestMatches = null;

            for (int i = 0; i < endOfSegmentPos; i++)
            {
                var matchInfo = nestedEval.Evaluate(remainingText);
                if (matchInfo.Success)
                {
                    break;
                }
                else
                {
                    matchedText.Append(remainingText[0]);
                    remainingText = remainingText.Substring(1);
                    // keep tabs on closest match.?
                    if ((bestMatches == null && matchInfo.Matches.Any()) || (bestMatches != null && bestMatches.Count < matchInfo.Matches.Length))
                    {
                        bestMatches = matchInfo.Matches.ToArray();
                        bestMatchText.Clear();
                        bestMatchText.Append(matchedText.ToString());
                    }
                }
            }

            this.Success = nestedEval.Success;
            if (nestedEval.Success)
            {
                // add all submathces.
                this.MatchedTokens.AddRange(nestedEval.MatchedTokens);
            }
            else
            {
                if (bestMatches.Any())
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
            if (currentChar < token.Start && currentChar > token.End)
            {
                return;
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
            if (currentChar < token.Start || currentChar > token.End)
            {
                return;
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
            if (!token.Characters.Contains(currentChar))
            {
                return;
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