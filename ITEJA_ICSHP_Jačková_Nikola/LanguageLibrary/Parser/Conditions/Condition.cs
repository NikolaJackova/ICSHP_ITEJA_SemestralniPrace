using LanguageLibrary.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLibrary.Parser.Conditions
{
    /// <summary>
    /// Abstract class for conditions
    /// </summary>
    public abstract class Condition : IASTItem
    {
        public Expression Left { get; private set; }

        public Condition(Expression expression) {
            Left = expression;
        }
        public abstract object Accept(IVisitor visitor);
    }
}
