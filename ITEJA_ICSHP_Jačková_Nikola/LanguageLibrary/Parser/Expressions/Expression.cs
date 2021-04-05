using LanguageLibrary.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    /// <summary>
    /// Abstract class for expressions
    /// </summary>
    public abstract class Expression : IASTItem
    {
        public abstract object Accept(IVisitor visitor);
    }
}
