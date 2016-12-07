﻿namespace DotNet.Globbing
{
    public class NumberRangeToken : RangeToken<char>
    {
        public override void Accept(IGlobTokenVisitor Visitor)
        {
            Visitor.Visit(this);
        }

    }
}