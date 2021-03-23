using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class Multiply : BinaryExpression
    {
        public Multiply(Expression left, Expression right) : base(left, right)
        {
            Operation = "*";
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitMultiply(this);
        }
    }
}
