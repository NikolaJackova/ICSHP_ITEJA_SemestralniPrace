using LanguageLibrary.Exceptions;
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

        public ExecutionContext()
        {
            Variables = new Dictionary<string, object>();
        }

        public object GetVariable(string key)
        {
            if (Variables.TryGetValue(key, out object value))
            {
                return value;
            }
            throw new LanguageException("Variable does not exists!");
        }

        public void SetVariable(string key, object value)
        {
            Variables[key] = value;
        }

        public void DeclareVariable(string key)
        {
            Variables[key] = default;
        }

        public bool ExistsVariable(string key)
        {
            return Variables.ContainsKey(key);
        }
    }
}
