namespace DotNet.Globbing
{
    public class GlobOptions
    {
        public GlobOptions()
        {
            Parsing = new ParsingOptions();
            Evaluation = new EvaluationOptions();           
        }

        public static GlobOptions Default = new GlobOptions();

        public ParsingOptions Parsing { get; set; }

        public EvaluationOptions Evaluation { get; set; }
    }
}
