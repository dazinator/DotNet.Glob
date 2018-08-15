namespace DotNet.Globbing
{
    public class GlobOptions
    {
        public GlobOptions()
        {
            Evaluation = new EvaluationOptions();           
        }

        public static GlobOptions Default = new GlobOptions();

        public EvaluationOptions Evaluation { get; set; }
    }
}
