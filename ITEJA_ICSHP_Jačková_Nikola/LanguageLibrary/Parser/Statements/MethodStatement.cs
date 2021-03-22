using LanguageLibrary.AST;
using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class MethodStatement : IStatement
    {
        public IdentExpression Identifier { get; private set; }

        public LinkedList<IExpression> Parameters { get; private set; }

        public MethodStatement(IdentExpression ident, LinkedList<IExpression> parameters)
        {
            Identifier = ident;
            Parameters = parameters;
        }

        public object Visit(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
