using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public abstract class Expression : IASTItem
    {
        public abstract object Accept(IVisitor visitor);
    }
}
