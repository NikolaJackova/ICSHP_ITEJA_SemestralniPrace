using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageLibrary.AST;
using LanguageLibrary.Parser.Expressions;

namespace LanguageLibrary.Parser.Conditions
{
    public class OneStatementCondition : Condition
    {
        public OneStatementCondition(IExpression expr) :base(expr)
        {
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Visit_OneStatement(this);
        }
    }
}
