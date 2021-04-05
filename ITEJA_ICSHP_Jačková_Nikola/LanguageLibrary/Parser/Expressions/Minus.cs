using LanguageLibrary.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class Minus : BinaryExpression
    {
        public Minus(Expression left, Expression right) : base(left, right)
        {
            Operation = "-";
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitMinus(this);
        }
    }
}
