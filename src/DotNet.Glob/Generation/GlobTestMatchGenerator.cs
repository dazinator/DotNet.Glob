using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Generation
{
    public class GlobMatchStringGenerator
    {
        private StringBuilder _stringBuilder;
        private Random _random;  
        private CompositeTokenMatchGenerator _generator;

       

        public GlobMatchStringGenerator(IEnumerable<IGlobToken> tokens)
        {
            _stringBuilder = new StringBuilder();
            _random = new Random();
            _generator = new CompositeTokenMatchGenerator(_random, tokens.ToArray());
            // _generators = new List<IMatchGenerator>(tokens.Count());
        }

        public string GenerateRandomMatch()
        {
            //_mode = MatchGenerationMode.Match;
            _stringBuilder.Clear();
            _generator.AppendMatch(_stringBuilder);
            return _stringBuilder.ToString();
        }

        public string GenerateRandomNonMatch()
        {
            //_mode = MatchGenerationMode.Match;
            _stringBuilder.Clear();
            _generator.AppendNonMatch(_stringBuilder);
            return _stringBuilder.ToString();
        }


    }
}