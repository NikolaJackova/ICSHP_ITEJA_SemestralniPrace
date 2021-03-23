using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class SetStatement : Statement
    {
        public IdentExpression Identifier { get; private set; }
        public Expression Expression { get; private set; }

        public SetStatement(IdentExpression identifier, Expression expression)
        {
            Identifier = identifier;
            Expression = expression;
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitSetStatement(this);
        }
    }
}
