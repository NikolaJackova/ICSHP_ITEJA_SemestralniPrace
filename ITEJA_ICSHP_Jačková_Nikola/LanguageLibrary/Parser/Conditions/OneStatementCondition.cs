using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LanguageLibrary.Parser.Expressions;

namespace LanguageLibrary.Parser.Conditions
{
    public class OneStatementCondition : Condition
    {
        public OneStatementCondition(Expression expr) :base(expr)
        {
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.Visit_OneStatement(this);
        }
    }
}
