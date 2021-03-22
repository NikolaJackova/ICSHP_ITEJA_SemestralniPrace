using LanguageLibrary.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class MinusUnary : UnaryExpression
    {
        public MinusUnary(IExpression expr) : base(expr)
        {
            Operation = "-";
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Visit_MinusUnary(this);
        }
    }
}
