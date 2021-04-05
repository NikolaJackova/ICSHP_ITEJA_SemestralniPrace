using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class ChangePenMethod : MethodStatement
    {
        public ChangePenMethod(IdentExpression expression, LinkedList<Expression> parameters) : base(expression, parameters)
        {
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitChangePenMethod(this);
        }
    }
}
