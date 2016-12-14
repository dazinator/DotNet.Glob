using System.Collections.Generic;
using System.Text.RegularExpressions;
using DotNet.Globbing;
using DotNet.Globbing.Token;
using DotNet.Globbing.Generation;

namespace DotNet.Glob.PerfTests
{
    public abstract class BaseGlobBenchMark
    {
        private Dictionary<string, List<string>> _testDataSet;

        protected BaseGlobBenchMark()
        {
            _testDataSet = new Dictionary<string, List<string>>();
        }

        public abstract string Pattern { get; set; }

        public List<string> TestStrings { get; set; }

        protected void InitialiseGlobTestData(string GlobPattern, int numberOfMatchingStrings, int numberOfNonMatchingStrings)
        {
            if (!_testDataSet.ContainsKey(GlobPattern))
            {
                // generate test data.
                var tokens = new GlobTokeniser().Tokenise(GlobPattern);
                var generator = new GlobMatchStringGenerator(tokens);
                
                int total = numberOfMatchingStrings + numberOfNonMatchingStrings;
                var testData = new List<string>(total);
                
                for (int i = 0; i < numberOfMatchingStrings; i++)
                {
                    testData.Add(generator.GenerateRandomNonMatch());
                }

                for (int i = 0; i < numberOfNonMatchingStrings; i++)
                {
                    testData.Add(generator.GenerateRandomNonMatch());
                }

                _testDataSet.Add(GlobPattern, testData);
            }

            TestStrings = _testDataSet[GlobPattern];
        }

        protected Regex CreateRegex(string GlobPattern, bool compiled)
        {
            var tokens = new GlobTokeniser().Tokenise(GlobPattern);
            return CreateRegex(tokens, compiled);
        }

        protected Regex CreateRegex(IList<IGlobToken> tokens, bool compiled)
        {

            var regexFormatter = new GlobToRegexFormatter();
            var regexString = regexFormatter.Format(tokens);
            var regex = new Regex(regexString, compiled ? RegexOptions.Compiled | RegexOptions.Singleline : RegexOptions.Singleline);
            return regex;
        }


    }
}