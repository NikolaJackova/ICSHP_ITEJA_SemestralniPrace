using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Interpreter
{
    public enum VarType
    {
        STRING,
        NUMBER
    }
    public class Variable
    {
        public object Value { get; private set; }
        public VarType Type { get; private set; }
        public Variable(VarType type, object value)
        {
            Value = value;
            Type = type;
        }
    }
}
