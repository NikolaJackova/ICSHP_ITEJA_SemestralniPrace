using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public abstract class UnaryExpression : Expression
    {
        public Expression Expression { get; protected set; }
        public string Operation { get; protected set; }

        public UnaryExpression(Expression expr)
        {
            Expression = expr;
        }

        public abstract override object Accept(IVisitor visitor);
    }
}
