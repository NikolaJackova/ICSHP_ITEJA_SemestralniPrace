using LanguageLibrary.Interpreter;
using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Variables
{
    public class VariableDeclaration : IASTItem
    {
        public IdentExpression Var { get; private set; }

        public VariableDeclaration(IdentExpression variable)
        {
            Var = variable;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitVariable(this);
        }
    }
}
