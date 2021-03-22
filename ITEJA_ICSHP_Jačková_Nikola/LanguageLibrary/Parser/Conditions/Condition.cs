using LanguageLibrary.AST;
using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Conditions
{
    public abstract class Condition : IASTItem
    {
        public IExpression Left { get; private set; }

        public Condition(IExpression expression) {
            Left = expression;
        }

        public abstract object Visit(IVisitor visitor);
    }
}
