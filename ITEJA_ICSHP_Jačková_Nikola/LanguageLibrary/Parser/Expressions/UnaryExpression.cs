using LanguageLibrary.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public abstract class UnaryExpression : IExpression
    {
        public IExpression Expression { get; protected set; }
        public string Operation { get; protected set; }

        public UnaryExpression(IExpression expr)
        {
            Expression = expr;
        }

        public abstract object Visit(IVisitor visitor);
    }
}
