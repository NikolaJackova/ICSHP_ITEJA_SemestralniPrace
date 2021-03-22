using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.AST
{
    public interface IASTItem
    {
        object Visit(IVisitor visitor);
    }
}
