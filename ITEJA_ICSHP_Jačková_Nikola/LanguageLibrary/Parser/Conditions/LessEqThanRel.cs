using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Conditions
{
    class LessEqThanRel : Condition
    {
        public LessEqThanRel(Expression left, Expression right) : base(left, right)
        {
            Operation = "<=";
        }
    }
}
