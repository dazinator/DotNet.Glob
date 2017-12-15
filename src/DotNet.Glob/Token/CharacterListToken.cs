using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Globbing.Token
{
    public class CharacterListToken : INegatableToken
    {
        private List<char> _charactersInvariantLowerCase;

        public CharacterListToken(char[] characters, bool isNegated)
        {
            Characters = new List<char>(characters);
            _charactersInvariantLowerCase = null;
            //  Characters = characters; // 
            IsNegated = isNegated;
        }
        public bool IsNegated { get; set; }

        //  public Char[] Characters { get; set; }

        public List<char> Characters { get; }

        public List<char> CharactersInvariantLowerCase
        {
            get
            {
                // initialize on first use (indicates case insensitive matching)
                if (_charactersInvariantLowerCase == null)
                    _charactersInvariantLowerCase = new List<char>(this.Characters.Select(c => char.ToLowerInvariant(c)));
                return _charactersInvariantLowerCase;
            }
        }

        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }

    }
}