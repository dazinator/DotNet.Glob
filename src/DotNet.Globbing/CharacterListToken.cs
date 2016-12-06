using System;

namespace DotNet.Globbing
{
    public class CharacterListToken : INegatableToken
    {
        public CharacterListToken(char[] characters)
        {
            Characters = characters;
        }
        public bool IsNegated { get; set; }

        public Char[] Characters { get; set; }

        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}