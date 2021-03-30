using LanguageLibrary.Exceptions;
using LanguageLibrary.Parser.Expressions;
using LanguageLibrary.Parser.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Interpreter
{
    public class Variables
    {
        public Dictionary<string, Variable> VariablesMap { get; private set; }
        public Variables()
        {
            VariablesMap = new Dictionary<string, Variable>();
        }
        public Variable GetVariable(string key)
        {
            if (VariablesMap.TryGetValue(key, out Variable value))
            {
                return value;
            }
            throw new LanguageException("Variable does not exists!");
        }

        public void SetVariable(string key, Variable value)
        {
            if (VariablesMap.ContainsKey(key))
            {
                VariablesMap[key] = value;
            } else
            {
                VariablesMap.Add(key, value);
            }
        }
        public void DeclareVariable(string key)
        {
            VariablesMap[key] = default;
        }

        public bool ExistsVariable(string key)
        {
            return VariablesMap.ContainsKey(key);
        }

        public bool HasVariableValue(string key)
        {
            if (VariablesMap.ContainsKey(key))
            {
                if (VariablesMap[key] != null) {
                    return true;
                }
            }
            return false;
        }
    }
}
