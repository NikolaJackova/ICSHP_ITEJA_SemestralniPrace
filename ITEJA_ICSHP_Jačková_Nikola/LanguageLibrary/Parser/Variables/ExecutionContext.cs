using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Variables
{
    class ExecutionContext<T>
    {
        private Dictionary<string, T> Variables;

        public ExecutionContext()
        {
            Variables = new Dictionary<string, T>();
        }


    }
}
