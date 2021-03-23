﻿using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Conditions
{
    public class GreaterEqThanRel : BinaryRelCondition
    {
        public GreaterEqThanRel(Expression left, Expression right) :base(left, right)
        {
            Operation = ">=";
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.Visit_GreaterEqThanRel(this);
        }
    }
}
