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
        private Dictionary<string, object> Variables;
        public Variables Vars { get; private set; }
        public ExecutionContext()
        {
            Variables = new Dictionary<string, object>();
            Vars = new Variables();
        }
    }
}
