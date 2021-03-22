using LanguageLibrary.AST;
using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Conditions
{
    public class NotEqualRel : BinaryRelCondition
    {
        public NotEqualRel(IExpression left, IExpression right) : base(left, right)
        {
            Operation = "!=";
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Visit_NotEqualRel(this);
        }
    }
}
