using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser
{
    public interface IASTItem
    {
        object Accept(IVisitor visitor);
    }
}
