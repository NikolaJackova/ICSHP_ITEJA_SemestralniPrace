using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public class PrintMethod : MethodStatement
    {
        public PrintMethod(LinkedList<Expression> parameters) : base(new IdentExpression("Print"), parameters)
        {
        }

        public override object Accept(IVisitor visitor)
        {
            return visitor.VisitPrintMethod(this);
        }
    }
}
