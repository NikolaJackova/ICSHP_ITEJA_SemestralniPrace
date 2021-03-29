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
        public LinkedList<Variable> VariablesList { get; private set; }

        public Dictionary<string, VarIdentExpression> VariablesMap { get; private set; }
        public Variables()
        {
            VariablesMap = new Dictionary<string, VarIdentExpression>();
            VariablesList = new LinkedList<Variable>();
        }
        public VarIdentExpression GetVariable(string key)
        {
            if (VariablesMap.TryGetValue(key, out VarIdentExpression value))
            {
                return value;
            }
            throw new LanguageException("Variable does not exists!");
        }

        public void SetVariable(string key, VarIdentExpression value)
        {
            VariablesMap[key] = value;
        }

        public void DeclareVariable(string key)
        {
            VariablesMap[key] = default;
        }

        public bool ExistsVariable(string key)
        {
            return VariablesMap.ContainsKey(key);
        }
    }
}
