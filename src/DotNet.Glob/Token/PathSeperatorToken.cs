using System;

namespace DotNet.Globbing.Token
{
    public class PathSeperatorToken : IGlobToken
    {
        public PathSeperatorToken(char value)
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