using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public class IdentExpression : Expression
    {
        public string Identifier { get; private set; }

        public IdentExpression(string ident)
        {
            Identifier = ident;
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.Visit_IdentExpression(this);
        }
    }
}
