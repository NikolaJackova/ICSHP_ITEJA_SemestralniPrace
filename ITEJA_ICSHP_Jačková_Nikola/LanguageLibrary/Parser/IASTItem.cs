using LanguageLibrary.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser
{
    /// <summary>
    /// Interface that implements each of the nodes of AST
    /// </summary>
    public interface IASTItem
    {
        object Accept(IVisitor visitor);
    }
}
