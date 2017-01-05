using System;
using System.IO;
using System.Linq;
using System.Text;

namespace DotNet.Globbing
{
    public class GlobStringReader : StringReader
    {

        private readonly string _Text;
        private int _CurrentIndex;
        public const int FailedRead = -1;
        public const char NullChar = (char)0;

        public const char ExclamationMarkChar = '!';
        public const char StarChar = '*';
        public const char OpenBracketChar = '[';
        public const char CloseBracketChar = ']';
        public const char DashChar = '-';
        public const char QuestionMarkChar = '?';
        //public const char DotChar = '.';
        //public const char SpaceChar = ' ';
        //public const char HashChar = '#';


        public static char[] AllowedNonAlphaNumericChars = new[] { '.', ' ', '!', '#', '-', ';', '=', '@', '~' };

        /// <summary>
        /// The current delimiters
        /// </summary>
        private static readonly char[] PathSeperators = new char[] { '/', '\\' };

        public GlobStringReader(string text) : base(text)
        {
            _Text = text;
            _CurrentIndex = -1;
        }

        /// <summary>
        /// The index of the current character
        /// </summary>
        public int CurrentIndex
        {
            get { return _CurrentIndex; }
            private set
            {
                _CurrentIndex = value;
                LastChar = _Text[_CurrentIndex - 1];
                CurrentChar = _Text[_CurrentIndex];
            }
        }

        public bool ReadChar()
        {
            return Read() != FailedRead;
        }

        public override int Read()
        {
            var result = base.Read();
            if (result != FailedRead)
            {
                _CurrentIndex++;
                LastChar = CurrentChar;
                CurrentChar = (char)result;
                return result;
            }

            return result;
        }

        public override int Read(char[] buffer, int index, int count)
        {
            var read = base.Read(buffer, index, count);
            CurrentIndex += read;
            CurrentChar = _Text[CurrentIndex];
            return read;
        }

        public override int ReadBlock(char[] buffer, int index, int count)
        {
            var read = base.ReadBlock(buffer, index, count);
            CurrentIndex += read;
            return read;
        }

        public override string ReadLine()
        {
            var readLine = base.ReadLine();
            if (readLine != null)
                CurrentIndex += readLine.Length;
            return readLine;
        }

        public string ReadPathSegment()
        {
            var segmentBuilder = new StringBuilder();
            //if (IsPathSeperator(CurrentChar))
            //{
            //    if (!ReadChar())
            //    {
            //        return segmentBuilder.ToString();
            //    }
            //    segmentBuilder.Append(CurrentChar);
            //}
            while (ReadChar())
            {
                if (!IsPathSeperator(CurrentChar))
                {
                    segmentBuilder.Append(CurrentChar);
                }
                else
                {
                    break;
                }
            }
            return segmentBuilder.ToString();
        }

        public override string ReadToEnd()
        {
            CurrentIndex = _Text.Length - 1;
            return base.ReadToEnd();
        }

        /// <summary>
        /// The previous character
        /// </summary>
        public char LastChar { get; private set; }

        /// <summary>
        /// The current character
        /// </summary>
        public char CurrentChar { get; private set; }

        /// <summary>
        /// Has the Command Reader reached the end of the file
        /// </summary>
        public bool HasReachedEnd
        {
            get { return Peek() == -1; }
        }

        /// <summary>
        /// Does current character match the character argument
        /// </summary>
        public bool IsCurrentCharEqualTo(char comparisonChar)
        {
            return IsCharEqualTo(CurrentChar, comparisonChar);
        }

        /// <summary>
        /// Are the arguments the same character, ignoring case
        /// </summary>
        public static bool IsCharEqualTo(char comparisonChar, char compareTo)
        {
            return char.ToLowerInvariant(comparisonChar) == char.ToLowerInvariant(compareTo);
        }

        /// <summary>
        /// Is current character WhiteSpace
        /// </summary>
        public bool IsWhiteSpace
        {
            get { return char.IsWhiteSpace(CurrentChar); }
        }

        /// <summary>
        /// Peek at the next character
        /// </summary>
        public char PeekChar()
        {
            if (HasReachedEnd)
            {
                return NullChar;
            }
            return (char)Peek();
        }

        /// <summary>
        /// Peek at the next character
        /// </summary>
        public bool TryPeek(int numberOfCharacters, out string result)
        {
            var currentIndex = CurrentIndex;
            if (currentIndex + numberOfCharacters >= _Text.Length)
            {
                result = null;
                return false;
            }

            result = _Text.Substring(CurrentIndex + 1, numberOfCharacters);
            return true;
        }

        public bool IsBeginningOfRangeOrList
        {
            get { return CurrentChar == OpenBracketChar; }
        }

        public bool IsEndOfRangeOrList
        {
            get { return CurrentChar == CloseBracketChar; }
        }

        public bool IsPathSeperator()
        {
            return IsPathSeperator(CurrentChar);


        }

        public static bool IsPathSeperator(char character)
        {

            var isCurrentCharacterStartOfDelimiter = IsCharEqualTo(character, PathSeperators[0]) ||
                                                     IsCharEqualTo(character, PathSeperators[1]);

            return isCurrentCharacterStartOfDelimiter;

        }

        public bool IsSingleCharacterMatch
        {
            get { return CurrentChar == QuestionMarkChar; }
        }

        public bool IsWildcardCharacterMatch
        {
            get { return CurrentChar == StarChar && PeekChar() != StarChar; }
        }

        public bool IsBeginningOfDirectoryWildcard
        {
            get { return CurrentChar == StarChar && PeekChar() == StarChar; }
        }

        public static bool IsValidLiteralCharacter(char character)
        {
            return Char.IsLetterOrDigit(character) || AllowedNonAlphaNumericChars.Contains(character);
        }

        internal bool IsValidLiteralCharacter()
        {
            return IsValidLiteralCharacter(CurrentChar);
        }
    }
}
