using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    class UnaryExpression
    {
        public Expression Expression { get; protected set; }
        public string Operation { get; protected set; }

        public UnaryExpression(Expression expr)
        {
            Expression = expr;
        }
    }
}
