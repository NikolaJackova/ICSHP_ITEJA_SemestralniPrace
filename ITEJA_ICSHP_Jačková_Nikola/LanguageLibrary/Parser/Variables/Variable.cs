using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Variables
{
    public enum VarType
    {
        STRING,
        NUMBER,
        NONE
    }
    public class Variable : IASTItem
    {
        public IdentExpression Var { get; private set; }

        public VarType Type { get; set; }
        public Variable(IdentExpression variable)
        {
            Var = variable;
            Type = VarType.NONE;
        }

        public Variable(IdentExpression variable, VarType type)
        {
            Var = variable;
            Type = type;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitVariable(this);
        }
    }
}
