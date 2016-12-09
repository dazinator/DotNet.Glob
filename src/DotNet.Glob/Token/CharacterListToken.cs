using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Globbing.Token
{
    public class CharacterListToken : INegatableToken
    {
        public CharacterListToken(char[] characters, bool isNegated)
        {
            Characters = new List<char>(characters);
            //  Characters = characters; // 
            IsNegated = isNegated;
        }
        public bool IsNegated { get; set; }

        //  public Char[] Characters { get; set; }

        public List<Char> Characters { get; set; }

        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }

    }
}