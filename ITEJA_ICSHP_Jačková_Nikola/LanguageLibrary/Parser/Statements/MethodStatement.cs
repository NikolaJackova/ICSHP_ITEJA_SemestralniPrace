using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class MethodStatement : Statement
    {
        public IdentExpression Identifier { get; private set; }

        public LinkedList<Expression> Parameters { get; private set; }

        public MethodStatement(IdentExpression ident, LinkedList<Expression> parameters)
        {
            Identifier = ident;
            Parameters = parameters;
        }
        public override object Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
