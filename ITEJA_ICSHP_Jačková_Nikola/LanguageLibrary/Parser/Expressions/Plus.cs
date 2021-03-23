using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class Plus : BinaryExpression
    {
        public Plus(Expression left, Expression right) : base(left, right)
        {
            Operation = "+";
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitPlus(this);
        }
    }
}
