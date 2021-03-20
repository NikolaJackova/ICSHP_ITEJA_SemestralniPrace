﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    class Multiply : BinaryExpression
    {
        public Multiply(Expression left, Expression right) : base(left, right)
        {
            Operation = "*";
        }
    }
}
