using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class PlusUnary : UnaryExpression
    {
        public PlusUnary(Expression expr) :base(expr)
        {
            Operation = "+";
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.Visit_PlusUnary(this);
        }
    }
}
