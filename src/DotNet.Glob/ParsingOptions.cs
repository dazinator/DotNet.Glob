namespace DotNet.Globbing
{
    public class ParsingOptions
    {
        public ParsingOptions()
        {
            AllowInvalidPathCharacters = false;
        }

        /// <summary>
        /// If true, allows non standard alpha-numeric characters that aren't valid for file system / directory paths, to be present in the token string.
        /// If you are comparing to arbitrary text and not file or directory paths, then you should set this to true.
        /// </summary>
        public bool AllowInvalidPathCharacters { get; set; }
    }
}
