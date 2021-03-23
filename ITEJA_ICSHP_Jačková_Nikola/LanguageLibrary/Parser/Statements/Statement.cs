using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Statements
{
    public abstract class Statement : IASTItem
    {
        public abstract object Accept(IVisitor visitor);
    }
}
