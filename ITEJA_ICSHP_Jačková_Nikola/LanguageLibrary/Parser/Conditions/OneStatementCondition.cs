using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LanguageLibrary.Parser.Expressions;

namespace LanguageLibrary.Parser.Conditions
{
    /// <summary>
    /// One statement condition is true when number is positive, false when 0 or negative
    /// Only number expressions are valid
    /// </summary>
    public class OneStatementCondition : Condition
    {
        public OneStatementCondition(Expression expr) :base(expr)
        {
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitOneStatement(this);
        }
    }
}
