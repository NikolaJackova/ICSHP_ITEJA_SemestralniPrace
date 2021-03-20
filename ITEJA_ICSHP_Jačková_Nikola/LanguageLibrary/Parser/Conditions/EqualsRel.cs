using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Conditions
{
    class EqualsRel : Condition
    {
        public EqualsRel(Expression left, Expression right) : base(left, right)
        {
            Operation = "==";
        }
    }
}
