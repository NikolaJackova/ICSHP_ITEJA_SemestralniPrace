using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Conditions
{
    class GreaterEqThanRel : Condition
    {
        public GreaterEqThanRel(Expression left, Expression right) :base(left, right)
        {
            Operation = ">=";
        }
    }
}
