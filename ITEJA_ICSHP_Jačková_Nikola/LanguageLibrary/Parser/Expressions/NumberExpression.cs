using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class NumberExpression : Expression
    {
        public double Value { get; private set; }

        public NumberExpression(double number) {
            Value = number;
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitNumberExpression(this);
        }
    }
}
