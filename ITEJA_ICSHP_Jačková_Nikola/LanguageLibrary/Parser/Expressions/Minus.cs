using LanguageLibrary.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class Minus : BinaryExpression
    {
        public Minus(IExpression left, IExpression right) : base(left, right)
        {
            Operation = "-";
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Visit_Minus(this);
        }
    }
}
