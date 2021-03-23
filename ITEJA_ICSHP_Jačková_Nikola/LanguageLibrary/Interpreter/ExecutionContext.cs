using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Interpreter
{
    class ExecutionContext<T>
    {
        private Dictionary<string, T> Variables;

        public ExecutionContext()
        {
            Variables = new Dictionary<string, T>();
        }

        public T GetVariable(string key)
        {
            if (Variables.TryGetValue(key, out T value))
            {
                return value;
            }
            throw new LanguageException("Variable does not exists!");
        }

        public void SetVariable(string key, T value)
        {
            Variables[key] = value;
        }
    }
}
