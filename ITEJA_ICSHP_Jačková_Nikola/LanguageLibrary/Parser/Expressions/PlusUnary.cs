using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    class PlusUnary : UnaryExpression
    {
        public PlusUnary(Expression expr) :base(expr)
        {
            Operation = "+";
        }
    }
}
