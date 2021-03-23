using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public abstract class BinaryExpression : Expression
    {
        public Expression Left { get; protected set; }
        public Expression Right { get; protected set; }
        public string Operation { get; protected set; }

        public BinaryExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public abstract override object Accept(IVisitor visitor);
    }
}
