﻿using LanguageLibrary.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class PlusUnary : UnaryExpression
    {
        public PlusUnary(IExpression expr) :base(expr)
        {
            Operation = "+";
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Visit_PlusUnary(this);
        }
    }
}
