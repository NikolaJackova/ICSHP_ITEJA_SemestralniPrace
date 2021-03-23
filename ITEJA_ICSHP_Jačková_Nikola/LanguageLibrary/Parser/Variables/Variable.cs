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
        private IdentExpression Var { get; set; }

        public Variable(IdentExpression variable)
        {
            Var = variable;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.Visit_Variable(this);
        }
    }
}
