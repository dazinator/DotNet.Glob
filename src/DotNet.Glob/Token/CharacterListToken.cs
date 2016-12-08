using System;

namespace DotNet.Globbing.Token
{
    public class CharacterListToken : INegatableToken
    {
        public CharacterListToken(char[] characters, bool isNegated)
        {
            Characters = characters;
            IsNegated = isNegated;
        }
        public bool IsNegated { get; set; }

        public Char[] Characters { get; set; }

        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}