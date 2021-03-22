using LanguageLibrary.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class NumberExpression : IExpression
    {
        public double Value { get; private set; }

        public NumberExpression(double number) {
            Value = number;
        }

        public object Visit(IVisitor visitor)
        {
            return visitor.Visit_NumberExpression(this);
        }
    }
}
