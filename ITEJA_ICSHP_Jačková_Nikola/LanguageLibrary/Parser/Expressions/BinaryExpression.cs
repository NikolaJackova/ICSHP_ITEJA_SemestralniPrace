using LanguageLibrary.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public abstract class BinaryExpression : IExpression
    {
        public IExpression Left { get; protected set; }
        public IExpression Right { get; protected set; }
        public string Operation { get; protected set; }

        public BinaryExpression(IExpression left, IExpression right)
        {
            Left = left;
            Right = right;
        }

        public abstract object Visit(IVisitor visitor);
    }
}
