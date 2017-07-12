namespace DotNet.Globbing
{
    public class GlobParseOptions
    {

        public GlobParseOptions()
        {
            AllowInvalidPathCharacters = false;
        }

        public static GlobParseOptions Default = new GlobParseOptions();

        /// <summary>
        /// If true, allows non standard alpha-numeric characters that aren't valid for file system / directory paths, to be present in the token string.
        /// If you are comparing to arbitrary text and not file or directory paths, then you should set this to true.
        /// </summary>
        public bool AllowInvalidPathCharacters { get; set; }

       
    }
}
