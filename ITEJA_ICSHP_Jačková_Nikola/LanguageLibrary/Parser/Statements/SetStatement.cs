using LanguageLibrary.AST;
using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class SetStatement : IStatement
    {
        public IdentExpression Identifier { get; private set; }
        public IExpression Expression { get; private set; }

        public SetStatement(IdentExpression identifier, IExpression expression)
        {
            Identifier = identifier;
            Expression = expression;
        }

        public object Visit(IVisitor visitor)
        {
            return visitor.Visit_SetStatement(this);
        }
    }
}
