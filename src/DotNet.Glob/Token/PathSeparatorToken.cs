using System;

namespace DotNet.Globbing.Token
{
    public class PathSeparatorToken : IGlobToken
    {
        public PathSeparatorToken(char value)
        {
            Value = value;
        }

        public void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }

        public char Value { get; set; }

      
    }
}