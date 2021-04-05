using LanguageLibrary.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class MinusUnary : UnaryExpression
    {
        public MinusUnary(Expression expr) : base(expr)
        {
            Operation = "-";
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitMinusUnary(this);
        }
    }
}
