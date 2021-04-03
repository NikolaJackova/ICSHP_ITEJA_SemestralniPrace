﻿using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class BackwardMethod : MethodStatement
    {
        public BackwardMethod(IdentExpression expression, LinkedList<Expression> parameters) : base(expression, parameters)
        {
        }
        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitBackwardMethod(this);
        }
    }
}
