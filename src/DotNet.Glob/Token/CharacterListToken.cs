namespace DotNet.Globbing.Token
{
    public class CharacterListToken : INegatableToken
    {
        public CharacterListToken(char[] characters, bool isNegated)
        {
            Characters = characters; // new List<char>(characters);                
            IsNegated = isNegated;
        }

        public bool IsNegated { get; set; }      

        public char[] Characters { get; }       

        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}