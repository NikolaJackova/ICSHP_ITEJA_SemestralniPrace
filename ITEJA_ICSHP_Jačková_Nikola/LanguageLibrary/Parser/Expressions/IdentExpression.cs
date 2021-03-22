using LanguageLibrary.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class IdentExpression : IExpression
    {
        public string Identifier { get; private set; }

        public IdentExpression(string ident)
        {
            Identifier = ident;
        }

        public object Visit(IVisitor visitor)
        {
            return visitor.Visit_IdentExpression(this);
        }
    }
}
