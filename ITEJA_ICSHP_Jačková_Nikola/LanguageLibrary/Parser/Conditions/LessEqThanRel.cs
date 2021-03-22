﻿using LanguageLibrary.AST;
using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Conditions
{
    public class LessEqThanRel : BinaryRelCondition
    {
        public LessEqThanRel(IExpression left, IExpression right) : base(left, right)
        {
            Operation = "<=";
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Visit_LessEqThanRel(this);
        }
    }
}
