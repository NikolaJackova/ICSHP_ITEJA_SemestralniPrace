using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    class Minus : BinaryExpression
    {
        public Minus(Expression left, Expression right) : base(left, right)
        {
            Operation = "-";
        }
    }
}
