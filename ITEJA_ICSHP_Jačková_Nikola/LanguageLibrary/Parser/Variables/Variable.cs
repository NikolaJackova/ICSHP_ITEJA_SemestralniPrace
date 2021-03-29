using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Variables
{
    public class Variable : IASTItem
    {
        public IdentExpression Var { get; private set; }

        public Variable(IdentExpression variable)
        {
            Var = variable;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitVariable(this);
        }
    }
}
