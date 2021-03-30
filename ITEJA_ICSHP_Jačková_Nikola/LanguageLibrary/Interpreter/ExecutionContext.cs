using LanguageLibrary.Exceptions;
using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Interpreter
{
    class ExecutionContext
    {
        public Variables Vars { get; private set; }
        public ExecutionContext()
        {
            Vars = new Variables();
        }
    }
}
