using LanguageLibrary.Parser.Expressions;
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
        public IdentExpression Identifier { get; private set; }
        public object Value { get; private set; }
        public VarType Type { get; private set; }
        public Variable(VarType type, object value, IdentExpression ident)
        {
            Value = value;
            Type = type;
            Identifier = ident;
        }
    }
}
