using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Expressions
{
    public enum VarType
    {
        STRING,
        NUMBER
    }
    public class VarIdentExpression : IdentExpression
    {
        public Expression Expression { get; private set; }
        public VarType Type { get; private set; }
        public VarIdentExpression(string ident, VarType type, Expression expression) : base(ident)
        {
            Expression = expression;
            Type = type;
        }
    }
}
