using LanguageLibrary.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class Plus : BinaryExpression
    {
        public Plus(IExpression left, IExpression right) : base(left, right)
        {
            Operation = "+";
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Visit_Plus(this);
        }
    }
}
